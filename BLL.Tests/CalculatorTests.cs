using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests
{
    
    public class CalculatorTests
    {
        //First Test
        [Fact]
        public void Add_TwoNumbersShouldAddNormallyHappyCase()
        {
            //Arrange
            var expected = 5;

            //Act
            var actual = Calculator.Add(2, 3);

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(expected == actual);
        }
       

        //Second test that includes multiple inline tests
        [Theory]
        [InlineData(4,3,7)]
        [InlineData(0,9, "zeros not allowed")]
        [InlineData(5.2,6.5, "cannot use doubles for addition")]
        public void Add_TwoNumbersShouldAddWithEdgeCases(dynamic x, dynamic y, dynamic expected)
        {
            //Act
            var actual = Calculator.Add(x, y);

            //Assert
            Assert.Equal(expected, actual);   
        }

        //Third test
        [Theory]
        [InlineData("4", 3)]
        public void Add_TwoNumbersShouldThrowExceptionIfOneIsString(string x, int y)
        {           
            //Assert
            Assert.Throws<ArgumentException>("x", () =>  Calculator.Add(x,y));
        }
    }
}
