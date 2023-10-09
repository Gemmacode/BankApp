using Core.Implementations;
using Data.Entities;


namespace GemmaBank.Test
{
    public class WithdrawTest
    { 
        [Fact]

        public void ValidWithdraw()
        {
            //Arrange
           Transactions transactions = new Transactions();            
            Account account = new Account();

            account.AccountBalance = 10000;

            decimal withdrawal_amount = 5000;
            decimal expected = 5000;
            
            //Act
            TransactionProcessing.ProcessWithdrawal(withdrawal_amount, account);
            decimal actual = account.AccountBalance;

            //Assert
            Assert.Equal(expected, actual);

        }

        //[Fact]
        //public void ValidWithdrawAmount()
        //{
        //    //Arrange
        //    Transactions transactions = new Transactions();
        //    Account account = new Account();
        //    account.AccountBalance = 10000;
        //    decimal withdrawal_amount = 5000;
        //    bool expected = true;

        //    //Act
        //    var actual = TransactionProcessing.ProcessWithdrawal(withdrawal_amount, account);

        //    //Assert
        //    Assert.Equal(expected, actual);
        //}
    }
}
