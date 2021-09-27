using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary.Test
{
    public class ExceptionLoggerTest
    {
        [Fact]
        public async void LogAsyncException_TooLongExceptionMessage_ThrowsOverflowException()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();

            var maxExceptionMessageLength = 150;
            var exception = new Exception(string.Join("", Enumerable.Range(0, maxExceptionMessageLength + 1).Select(x => "_")));
            Func<Task> func = () => exceptionLogger.LogAsync(exception);

            //Act & Assert
            //await Assert.ThrowsAsync<OverflowException>(func);

            //Act
            var result = await Record.ExceptionAsync(func);

            //Assert
            Assert.IsType<OverflowException>(result);
            Assert.Equal($"MaxLength = {maxExceptionMessageLength}", result.Message);
        }

        [Fact]
        public async void LogAsyncException_Call_IsCompletedSuccessfully()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();

            var exception = new Exception();

            //Act
            var task = exceptionLogger.LogAsync(exception);
            await task;

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async void LogAsyncException_PassExceptionWithMessage_LoggedSameMessage()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();
            var expectedResult = Guid.NewGuid().ToString();
            var exception = new Exception(expectedResult);

            //Act
            await exceptionLogger.LogAsync(exception);
            var result = exceptionLogger.End();

            //Assert
            Assert.Contains(expectedResult, result);
        }

        [Fact]
        public async void LogAsyncAggregateException_PassExceptionWithMessage_LoggedSameMessage()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            var expectedResult = "ExceptionLoggerTest";
            var exception = new Exception(expectedResult);
            var aggregateException = new AggregateException(exception);

            //Act
            var result = await exceptionLogger.LogAsync(aggregateException);

            //Assert
            Assert.Contains(expectedResult, result);
        }

        [Fact]
        public void End_MessageLoggedEvent_SenderIsExceptionLogger()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();

            object eventSender = null;
            exceptionLogger.MessageLogged += (sender, args) => eventSender = sender;

            //Act
            exceptionLogger.End();

            //Assert
            Assert.Equal(exceptionLogger, eventSender);
        }

        [Fact]
        public void End_MessageLoggedEvent_ResurnsMessageEventArgs()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();

            EventArgs expectedArgs = null;
            exceptionLogger.MessageLogged += (sender, args) => expectedArgs = args;

            //Act
            exceptionLogger.End();

            //Assert
            Assert.IsType<ExceptionLogger.MessageEventArgs>(expectedArgs);
        }

        [Fact]
        public void End_MessageLoggerInvoked()
        {
            //Arrange
            var exceptionLogger = new ExceptionLogger();
            exceptionLogger.Begin();

            bool result = false;
            exceptionLogger.MessageLogged += (sender, args) => result = true;

            //Act
            exceptionLogger.End();

            //Assert
            Assert.True(result);
        }
    }
}
