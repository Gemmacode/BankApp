using Data.Entities;

namespace Core.Implementations
{
    public class TransactionProcessing
    {

        public static List<Account> AllAccounts = new List<Account>() { };

        public static List<AccountStatement> AllAccountStatements = new List<AccountStatement>() { };

        public static Account ProcessWithdrawal(decimal withdrawalAmount, Account accountExist)
        {

            // Find the account based on the user's ID and provided account number

            if (accountExist != null)
            {

                if (withdrawalAmount > 0 && withdrawalAmount <= accountExist.AccountBalance)
                {
                    accountExist.AccountBalance -= withdrawalAmount;
                    Help.UpdateAccount(accountExist);
                    Console.WriteLine($"Successfully withdrew {withdrawalAmount} from account number: {accountExist.AccountNumber}");

                    return accountExist;
                }
                else
                {
                    throw new Exception("Invalid withdrawal amount. Make sure the amount is within your available balance.");
                }

            }
            else
            {
                throw new Exception("Account not found or you don't have permission to withdraw from this account.");
            }
        }

        public static bool TryGetDepositAmount(string amountInput, out decimal depositAmount)
        {
            if (decimal.TryParse(amountInput, out depositAmount))
            {
                return true; // Input is valid.
            }
            else
            {
                depositAmount = 0;
                return false; // Input is invalid.
            }
        }
        public static void PerformTransfer(User loggedInUser, Account sender, Account receiver, decimal transferAmount)
        {
            if (transferAmount > 0 && transferAmount <= sender.AccountBalance)
            {
                sender.AccountBalance -= transferAmount;
                var transactions = new Transactions();
                Help.UpdateAccount(sender);
                transactions.Create_a_senderTransferStatement(loggedInUser, sender.AccountType, sender.AccountNumber, receiver.AccountNumber, transferAmount, sender.AccountBalance);

                receiver.AccountBalance += transferAmount;
                Help.UpdateAccount(receiver);
                transactions.Create_a_receiverTransferStatement(loggedInUser, receiver.AccountType, sender.AccountNumber, receiver.AccountNumber, transferAmount, receiver.AccountBalance);
                Console.WriteLine("Transaction Successful");
            }
            else
            {
                Console.WriteLine("Invalid transfer amount. Make sure the amount is within your available balance.");
            }
        }


    }
}
