using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class AddAccount
    {
        [Fact]
        public void SendCustomerDataToJsonFile_ShouldCreateFileAndAppendData()
        {
            // Arrange
            string filePath = "test2.json"; // Provide a test file path
            Account account = new()
            {
                AccountNumber = "123456789"
            };

            // Act
             Help.SendAccountDataToJsonFile(account, filePath);

            // Assert
            // Check if the file exists after running the method
            Assert.True(File.Exists(filePath));

            // Optionally, you can read the file and validate its content
            string fileContent = File.ReadAllText(filePath);
            Assert.Contains("123456789", fileContent); // Replace with your expected data
        }
    }
}
