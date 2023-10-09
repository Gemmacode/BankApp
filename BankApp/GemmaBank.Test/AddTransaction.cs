using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class AddTransaction
    {

        [Fact]
        public void SendTransactionDataToJsonFile_ShouldCreateFileAndAppend()
        {
            // Arrange
            string filePath = "test3.json"; // Provide a test file path
            AccountStatement accountStatement = new()
            {
                AccountOwner = "Gemma Abdul"
            };

            // Act
            Help.SendAccountStatementDataToJsonFile(accountStatement, filePath);

            // Assert
            // Check if the file exists after running the method
            Assert.True(File.Exists(filePath));

            // Optionally, you can read the file and validate its content
            string fileContent = File.ReadAllText(filePath);
            Assert.Contains("Gemma Abdul", fileContent);

        }
    }
}
