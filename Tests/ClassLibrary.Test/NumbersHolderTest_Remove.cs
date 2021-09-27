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
        public async void RemoveAsync_RemoveSingleValue_ExpectsEmptyArray()
        {
            //Arrange
            var numbersHolder = new NumbersHolder();
            int number = default;
            numbersHolder.Add(number);

            //Act
            await numbersHolder.RemoveAsync();

            //Assert
            Assert.Equal(0, numbersHolder.Count);
        }
    }
}
