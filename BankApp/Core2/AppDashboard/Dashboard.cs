using Core.Implementations;
using Core.Services;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AppDashboard
{
    public class Dashboard
    {
         
        
        public Dashboard()
        {

        }

        

        public void MyDashBoard(User LoggedinUser)
        {
            var transact = new Transactions();
            Console.Clear();
            Console.WriteLine($"Welcome {LoggedinUser.FullName}");

            Console.WriteLine("Press 1 to Create Account");
            Console.WriteLine("Press 2 to  Deposit");
            Console.WriteLine("Press 3 Withdraw");
            Console.WriteLine("Press 4 Transfer");
            Console.WriteLine("Press 5 to CheckBalance");
            Console.WriteLine("Press 6 to show my accounts");
            Console.WriteLine("Press 7 to get Account Statement");
            Console.WriteLine("Press 8 to logout");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
         
                transact.CreateAccount(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "2")
            {
                transact.Deposit(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "3")
            {
                transact.ProcessWithdrawal(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "4")
            {
                transact.Transfer(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "5")
            {
                transact.CheckBalance(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "6")
            {
                transact.GetAllAccount(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "7")
            {
                transact.GetMyAccountStatement(LoggedinUser);
                MyDashBoard(LoggedinUser);
            }
            else if (choice == "8")
            {
                var rep = new Auth();
                rep.Logout();
               
            }
            else 
            {
                Console.WriteLine("Invalid option");
                Console.ReadKey();
                MyDashBoard(LoggedinUser);
            }
        }
    }
}
