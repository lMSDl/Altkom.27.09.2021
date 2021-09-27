using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary.Test
{
    public partial class NumbersHolderTest
    {
        [Fact]
        public void NumbersHolder_DefaultConstructor_CreatesEmptyArray()
        {
            //Arrange
            var numbersHolder = new NumbersHolder();

            //Act
            var result = numbersHolder.Fetch();

            //Assert
            Assert.Empty(result);
        }


        [Theory]
        [InlineData(4, 2, 7, 9, 1, 5, 2, 7, 2, 12)]
        public void Sort_ExecutesBelow1sec(params int[] numers)
        {
            //Arrange
            var numbersHolder = new NumbersHolder();
            foreach (var item in numers)
            {
                numbersHolder.Add(item);
            }
            Stopwatch stopwatch = new Stopwatch();

            //Act
            stopwatch.Start();
            numbersHolder.Sort();
            stopwatch.Stop();

            //Assert
            Assert.True(stopwatch.Elapsed < TimeSpan.FromMilliseconds(0.5));
        }
    }
}
