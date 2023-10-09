using Core.Implementations;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemmaBank.Test
{
    public class TransferTest
    {
        [Fact]
        public void PerformTransfer_ValidTransfer()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var sender = new Account { UserID = Guid.NewGuid(), AccountNumber = "Sender123", AccountBalance = 1000.0m };
            var receiver = new Account { UserID = Guid.NewGuid(), AccountNumber = "Receiver456", AccountBalance = 500.0m };
            decimal transferAmount = 300.0m;
           
            // Act
            TransactionProcessing.PerformTransfer(loggedInUser, sender, receiver, transferAmount);

            // Assert
            // Verify that sender's balance is reduced by the transferAmount
            Assert.Equal(700.0m, sender.AccountBalance);

            // Verify that receiver's balance is increased by the transferAmount
            Assert.Equal(800.0m, receiver.AccountBalance);
        }

        [Fact]
        public void PerformTransfer_InvalidTransferAmount()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var sender = new Account { UserID = Guid.NewGuid(), AccountNumber = "Sender123", AccountBalance = 1000.0m };
            var receiver = new Account { UserID = Guid.NewGuid(), AccountNumber = "Receiver456", AccountBalance = 500.0m };
            decimal transferAmount = -100.0m; // Invalid transfer amount
            
            // Act
            TransactionProcessing.PerformTransfer(loggedInUser, sender, receiver, transferAmount);

            // Assert
            // Verify that sender's balance remains unchanged
            Assert.Equal(1000.0m, sender.AccountBalance);

            // Verify that receiver's balance remains unchanged
            Assert.Equal(500.0m, receiver.AccountBalance);
        }

        [Fact]
        public void PerformTransfer_InsufficientBalance()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var sender = new Account { UserID = Guid.NewGuid(), AccountNumber = "Sender123", AccountBalance = 1000.0m };
            var receiver = new Account { UserID = Guid.NewGuid(), AccountNumber = "Receiver456", AccountBalance = 500.0m };
            decimal transferAmount = 1200.0m; 
            
            // Act
            TransactionProcessing.PerformTransfer(loggedInUser, sender, receiver, transferAmount);

            // Assert
            // Verify that sender's balance remains unchanged
            Assert.Equal(1000.0m, sender.AccountBalance);

            // Verify that receiver's balance remains unchanged
            Assert.Equal(500.0m, receiver.AccountBalance);
        }
    }
}
