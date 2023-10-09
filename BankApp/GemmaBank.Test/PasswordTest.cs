using Core.Implementations.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class PasswordTest
    {
        [Theory]
        [InlineData(false, "gemosky4310")]
        [InlineData(false, "gem@4310")]
        [InlineData(false, "gem@gmail.com")]
        [InlineData(false, "gem")]
        [InlineData(true, "Gems43!#")]

        public void InValidPassword_TestCases(bool expected, string password)
        {
            var actual = AuthValidations.IsValidPassword(password);
            Assert.Equal(expected, actual);
        }
    }
}
