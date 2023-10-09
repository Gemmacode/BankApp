using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class ReadCustomerFromJsonFile
    {
        [Fact]
        public void ReadCustomersFromJsonFile_ShouldDeserializeAndReturnCustomers()
        {
            // Arrange
            string filePath = "test.json"; // Provide a test file path
            File.WriteAllText(filePath, "{'FullName': 'Gemma Abdul'}");

            // Act
            List<User> customers = Help.ReadCustomersFromJsonFile(filePath);

            // Assert
            Assert.NotNull(customers);
            Assert.Single(customers);

            // Optionally, you can validate specific customer properties
            Assert.Equal("Gemma Abdul", customers[0].FullName);


            // Cleanup: Delete the test file
            File.Delete(filePath);
        }
    }
}
