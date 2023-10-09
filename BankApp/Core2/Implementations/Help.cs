using Data.Entities;
using Newtonsoft.Json;


namespace Core.Implementations
{
   public class Help
    {
        public static void SendCustomerDataToJsonFile(User user, string filePath)
        {
            // Serialize customer object to JSON
            string jsonCustomer = JsonConvert.SerializeObject(user);
            
            // Check if the file exists, and create it if it doesn't
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            // Append customer data to the JSON file
            File.AppendAllText(filePath, jsonCustomer + Environment.NewLine);
        }

        public static void SendAccountDataToJsonFile(Account account, string filePath)
        {
            // Serialize customer object to JSON
            string jsonCustomer = JsonConvert.SerializeObject(account);

            // Check if the file exists, and create it if it doesn't
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            // Append customer data to the JSON file
            File.AppendAllText(filePath, jsonCustomer + Environment.NewLine);
        }

        public static List<User> ReadCustomersFromJsonFile(string filePath)
        {
            List<User> customers = new List<User>();



            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            // Deserialize each line as a Customer object
                            User customer = JsonConvert.DeserializeObject<User>(line);
                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }



            return customers;
        }

        public static List<Account> ReadAccountsFromJsonFile(string filePath)
        {
            List<Account> accounts = new List<Account>();



            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            // Deserialize each line as a Customer object
                            Account account = JsonConvert.DeserializeObject<Account>(line);
                            accounts.Add(account);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }



            return accounts;
        }


        public static void UpdateAccount(Account accountExist)

        {
            var allAccounts = Help.ReadAccountsFromJsonFile("Account.json");

            // Check if the record for myAccount already exists

            int existingIndex = allAccounts.FindIndex(account => account.AccountNumber == accountExist.AccountNumber);

            if (existingIndex != -1)

            {
                allAccounts.RemoveAt(existingIndex);
            }

            allAccounts.Add(accountExist);

            // Serialize account object to JSON

            using (StreamWriter writer = new StreamWriter("Account.json", false))

            {
                writer.Write(string.Empty);
            }

            foreach (var item in allAccounts)

            {
                string jsonAccount = JsonConvert.SerializeObject(item);

                // Check if the file exists, and create it if it doesn't

                if (!File.Exists("Account.json"))
                {
                    using (File.Create("Account.json")) { }
                }

                // Append account data to the JSON file

                File.AppendAllText("Account.json", jsonAccount + Environment.NewLine);

            }

        }

        public static void SendAccountStatementDataToJsonFile(AccountStatement statement, string filePath)
        {
            // Serialize account statement object to JSON
            string jsonAccountStatement = JsonConvert.SerializeObject(statement);

            // Check if the file exists, and create it if it doesn't
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            // Append account statement data to the JSON file
            File.AppendAllText(filePath, jsonAccountStatement + Environment.NewLine);

        }

        public static List<AccountStatement> ReadAccountStatementFromJsonFile(string filePath)
        {
              List<AccountStatement> accountStatement = new List<AccountStatement>();



                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                            // Deserialize each line as a Customer object
                            AccountStatement accountStatements = JsonConvert.DeserializeObject<AccountStatement>(line);
                                accountStatement.Add(accountStatements);
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading JSON file: {ex.Message}");
                }



                return accountStatement;
            


        }

    }
}
