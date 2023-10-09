using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class ReadAccountFromJsonFile
    {
        [Fact]
        public void ReadCustomersFromJsonFile_ShouldDeserializeAndReturnCustomers()
        {
            // Arrange
            string filePath = "test2.json"; // Provide a test file path
            File.WriteAllText(filePath, "{'AccountNumber': '123456'}");

            // Act
            List<Account> accounts = Help.ReadAccountsFromJsonFile(filePath);

            // Assert
            Assert.NotNull(accounts);


            // Optionally, you can validate specific customer properties
            Assert.Equal("123456", accounts[0].AccountNumber);


            // Cleanup: Delete the test file
            File.Delete(filePath);
        }
    }
}
