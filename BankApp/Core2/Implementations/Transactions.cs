using Core.Services;
using Data.Entities;
using Data.Enum;


namespace Core.Implementations
{
    public class Transactions : ITransactions
    {

        public void CreateAccount(User loggedInUser)
        {
            Console.WriteLine("Choose an Account type");
            Console.WriteLine("Press 1 to select Current");
            Console.WriteLine("Press 2 for Savings");

            string choice = Console.ReadLine();

            var random = new Random();
            var accNo = random.Next(1504070908, 2099999999).ToString();

            if (choice == "1")
            {
                var createAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = accNo,
                    AccountType = AccountType.Current,
                    AccountBalance = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserID = loggedInUser.Id
                };
                Help.SendAccountDataToJsonFile(createAccount, "Account.json");
                Console.WriteLine("Your Current account was created successfully");
            }
            else if (choice == "2")
            {
                decimal amount;

                do
                {
                    Console.WriteLine("Please enter an initial deposit of at least 1000 naira");
                } while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 1000);

                var createAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = accNo,
                    AccountType = AccountType.Savings,
                    AccountBalance = amount,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserID = loggedInUser.Id
                };
                Help.SendAccountDataToJsonFile(createAccount, "Account.json");
                Console.WriteLine("Your Savings account was created successfully");
                Create_a_DepositStatement(loggedInUser, AccountType.Savings, accNo, amount, createAccount.AccountBalance);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose a valid account type.");
                CreateAccount(loggedInUser);
            }

            Console.ReadKey();
        }



        public void ProcessWithdrawal(User loggedInUser)
        {
            try
            {
                Console.WriteLine("Enter your Account Number");
                string accNo = Console.ReadLine();
                Console.WriteLine("Enter the amount to withdraw");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal withdrawalAmount))
                {
                    List<Account> accounts = Help.ReadAccountsFromJsonFile("Account.json");
                    var accountExist = accounts.Find(account => account.UserID == loggedInUser.Id && account.AccountNumber == accNo);
                    if (accountExist != null)
                    {

                        var result = TransactionProcessing.ProcessWithdrawal( withdrawalAmount, accountExist);
                        Create_a_WithDrawalStatement(loggedInUser, result.AccountType, result.AccountNumber, withdrawalAmount, result.AccountBalance);

                    }
                    else
                    {
                        Console.WriteLine("Account not found or you don't have permission to withdraw from this account.");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }

        public void Deposit(User loggedInUser)
        {
            Console.WriteLine("Please enter the account number");
            string accNo = Console.ReadLine();

            // Find the account based on the user's ID and provided account number
            List<Account> accounts = Help.ReadAccountsFromJsonFile("Account.json");
            var accountExist = accounts.Find(account => account.UserID == loggedInUser.Id && account.AccountNumber == accNo);

            if (accountExist != null)
            {
                Console.WriteLine("Enter an Amount to Deposit");
                string amountInput = Console.ReadLine();

                if (TransactionProcessing.TryGetDepositAmount(amountInput, out decimal depositAmount))
                {
                    if (depositAmount > 0)
                    {
                        accountExist.AccountBalance += depositAmount;
                        Help.UpdateAccount(accountExist);
                        Console.WriteLine($"Successfully deposited {depositAmount} to account number: {accNo}");
                        Create_a_DepositStatement(loggedInUser, accountExist.AccountType, accountExist.AccountNumber, depositAmount, accountExist.AccountBalance);
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount. Please enter a positive amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }
            }
            else
            {
                Console.WriteLine("Account not found or you don't have permission to deposit into this account.");
            }

            Console.ReadKey();
        }



        public void Transfer(User loggedInUser)
        {
            Console.WriteLine("Enter Sender Account number");
            string senderAccNo = Console.ReadLine();

            Console.WriteLine("Enter Receiver Account number");
            string receiverAccNo = Console.ReadLine();

            List<Account> accounts = Help.ReadAccountsFromJsonFile("Account.json");
            var sender = accounts.Find(account => account.UserID == loggedInUser.Id && account.AccountNumber == senderAccNo);          
            var receiver = accounts.Find(account => account.UserID == loggedInUser.Id && account.AccountNumber == receiverAccNo);

            if (sender != null && receiver != null)
            {
                Console.WriteLine("Enter amount to transfer");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal transferAmount))
                {
                    TransactionProcessing.PerformTransfer(loggedInUser, sender, receiver, transferAmount);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }
            }
            else
            {
                Console.WriteLine("One or both of the accounts were not found.");
            }

            Console.ReadKey();
        }



        public void CheckBalance(User loggedInUser)
        {
            Console.WriteLine("Please enter the account number");
            string accNoInput = Console.ReadLine();

            if (int.TryParse(accNoInput, out int accNo))
            {
                List<Account> accounts = Help.ReadAccountsFromJsonFile("Account.json");
                var accountExist = accounts.Find(account => account.UserID == loggedInUser.Id && account.AccountNumber == accNo.ToString());

                if (accountExist != null)
                {
                    Console.WriteLine($"Your Account Balance is {accountExist.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Account not found or you don't have permission to access this account.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric account number.");
            }

            Console.ReadKey();
        }





        public void GetAllAccount(User loggedInUser)
        {

            List<Account> accounts = Help.ReadAccountsFromJsonFile("Account.json");
            var loggedINUserAccounts = accounts.Where(account => account.UserID == loggedInUser.Id).ToList();

            Console.WriteLine("|----------------------|---------------------|----------------------|----------------------|");
            Console.WriteLine("|         NAME         |    ACCOUNT NUMBER   |      ACCOUNT TYPE    |    ACCOUNT BALANCE   |");
            Console.WriteLine("|----------------------|---------------------|----------------------|----------------------|");

            foreach (var account in loggedINUserAccounts)
            {
                Console.WriteLine($"| {loggedInUser.FullName,-20} | {account.AccountNumber,-20} | {account.AccountType,-20} | {account.AccountBalance,-20} |");
                Console.WriteLine("|----------------------|---------------------|----------------------|----------------------|");
            }
            
            Console.ReadKey();
        }



        public void Create_a_DepositStatement(User loggedInUser, AccountType accType, string AccountNo, decimal amount, decimal balance)
        {
            var depositStatement = new AccountStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = AccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Credit,
                Description = $"Deposit occured",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id

            };
            Help.SendAccountStatementDataToJsonFile(depositStatement, "AccountStatement.json");
        }


        public void Create_a_WithDrawalStatement(User loggedInUser, AccountType accType, string AccountNo, decimal amount, decimal balance)
        {
            var withdrawalStatement = new AccountStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = AccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Debit,
                Description = $"Withdrawal occured",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id

            };
            Help.SendAccountStatementDataToJsonFile(withdrawalStatement, "AccountStatement.json");
        }



        public void Create_a_senderTransferStatement(User loggedInUser, AccountType accType, string senderAccountNo, string receiverAccountNo, decimal amount, decimal balance)
        {

            var createdDebitStatement = new AccountStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = senderAccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Debit,
                Description = $"Transfered {amount} to {receiverAccountNo}",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id
            };
            Help.SendAccountStatementDataToJsonFile(createdDebitStatement, "AccountStatement.json");
        }


        public void Create_a_receiverTransferStatement(User loggedInUser, AccountType accType, string senderAccountNo, string receiverAccountNo, decimal amount, decimal balance)
        {

            var createdCreditStatement = new AccountStatement()
            {
                Date = DateTime.UtcNow,
                AccountOwner = loggedInUser.FullName,
                AccountNo = receiverAccountNo,
                AccountType = accType,
                Amount = amount,
                Balance = balance,
                CashFlow = CashFlow.Credit,
                Description = $"Received {amount} from {senderAccountNo}",
                Id = Guid.NewGuid().ToString(),
                UserId = loggedInUser.Id
            };

            Help.SendAccountStatementDataToJsonFile(createdCreditStatement, "AccountStatement.json");

        }



        public void GetMyAccountStatement(User loggedInUser)
        {
            Console.WriteLine("Enter an account Number");
            string accNoInput = Console.ReadLine();

            if (!int.TryParse(accNoInput, out int accNo))
            {
                Console.WriteLine("Invalid account number. Please enter a valid numeric account number.");
                Console.ReadKey();
                return;
            }

            var allAccountStatement = Help.ReadAccountStatementFromJsonFile("AccountStatement.json");

            var myAccountStatements = allAccountStatement.Where(statement => statement.UserId == loggedInUser.Id && statement.AccountNo == accNo.ToString()).ToList();

            if (myAccountStatements.Count == 0)
            {
                Console.WriteLine("No account statements found for the provided account number.");
                Console.ReadKey();
                return;
            }

            var firstStatement = myAccountStatements.FirstOrDefault();
            if (firstStatement != null)
            {
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
                Console.WriteLine($"| Account Owner: {firstStatement.AccountOwner,-47} | Account Number: {firstStatement.AccountNo,-41} | Account Type: {firstStatement.AccountType,-44} |");
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            }

            Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            Console.WriteLine("|         DATE         |      CASH FLOW      |       AMOUNT         |    DESCRIPTION      |       BALANCE        |");
            Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");

            foreach (var statement in myAccountStatements)
            {
                Console.WriteLine($"| {statement.Date,-20} | {statement.CashFlow,-20} | {statement.Amount,-20} | {statement.Description,-20} | {statement.Balance,-20} |");
                Console.WriteLine("|----------------------|----------------------|----------------------|----------------------|----------------------|");
            }

            Console.ReadKey();
        }











    }
}
