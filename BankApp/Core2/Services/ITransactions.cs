using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITransactions
    {
        void CreateAccount(User loggedInUser);      
        void ProcessWithdrawal(User loggedInUser );
        void Deposit(User loggedInUser );
        void Transfer(User loggedInUser );

    }
}
