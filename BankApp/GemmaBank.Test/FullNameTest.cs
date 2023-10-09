using Core.Implementations;
using Core.Implementations.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class FullNameTest
    {
        [Fact]

        public void ValidFullName_Test()
        {
            // AAA

            // Arrange
            var fullname = "James Gem";

            // Act
            var actual = AuthValidations.IsValidFullName(fullname);
            var expected = true;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidFullName_Test()
        {
            var fullname = "JamesGem";
            var actual = AuthValidations.IsValidEmail(fullname);
            var expected = false;   
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "James Gem")]
        [InlineData(false, "James gem")]
        [InlineData(false, "james Gem")]
        [InlineData(false, "JamesGem")]
        [InlineData(false, "123 Gem")]
        [InlineData(false, "James123 Gem")]
        [InlineData(false, "James123! gem!")]

        public void FullName_TestCases(bool expected, string fullname)
        {
            // Act
            var actual = AuthValidations.IsValidFullName(fullname);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

