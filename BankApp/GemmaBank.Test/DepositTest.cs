
using Data.Entities;
using Core.Implementations;

namespace GemmaBank.Test
{
    public class DepositTest
    {
        
        [Fact]
        public void TryGetDepositAmount_ValidInput()
        {
            string amountInput = "1000.0";
            decimal depositAmount;

            // Act
            bool result = TransactionProcessing.TryGetDepositAmount(amountInput, out depositAmount);

            // Assert
            Assert.True(result);
            Assert.Equal(1000.0m, depositAmount);
        }

        [Fact]
        public void TryGetDepositAmount_InvalidInput()
        {
            string amountInput = "InvalidInput";
            decimal depositAmount;

            // Act
            bool result = TransactionProcessing.TryGetDepositAmount(amountInput, out depositAmount);

            // Assert
            Assert.False(result);
            Assert.Equal(0.0m, depositAmount);
        }
    }

}

