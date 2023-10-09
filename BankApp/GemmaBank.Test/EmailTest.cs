using Core.Implementations.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class EmailTest
    {
        [Theory]
        [InlineData(true, "gem@gmail.com")]
        [InlineData(true, "Gem@gmail.com")]
        [InlineData(true, "gem32@gmail.com")]
        [InlineData(false, "gem!@gmail.com")]
        [InlineData(false, "gemgmail.com")]
        [InlineData(false, "gem @gmail.com")]

        public void InValidEmail_TestCases(bool expected, string email)
        {
            var actual = AuthValidations.IsValidEmail(email);
            Assert.Equal(expected, actual);
        }
    }
}
