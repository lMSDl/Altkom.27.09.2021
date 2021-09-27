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
        public void Add_PassSingleValue_ExpectsSingleValueArrayWithSameValue()
        {
            //Arrange
            var numbersHolder = new NumbersHolder();
            int number = default;

            //Act
            numbersHolder.Add(number);
            var result = numbersHolder.Fetch();

            //Assert
            //Assert.Equal(new[] { number }, result);
            //Assert.Single(result);
            Assert.Single(result, number);
        }

        
    }
}
