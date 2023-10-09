using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class AddCustomer
    {
        [Fact]
        public void SendCustomerDataToJsonFile_ShouldCreateFileAndAppendData()
        {
            // Arrange
            string filePath = "test.json"; // Provide a test file path
            User user = new()
            {

                FullName = "Gemma Abdul"

            };

            // Act
             Help.SendCustomerDataToJsonFile(user, filePath);

            // Assert
            // Check if the file exists after running the method
            Assert.True(File.Exists(filePath));

            // Optionally, you can read the file and validate its content
            string fileContent = File.ReadAllText(filePath);
            Assert.Contains("Gemma Abdul", fileContent); // Replace with your expected data
        }
    }
}
