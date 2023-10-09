using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuth
    {
        void SignUp();
        User Login();
        void Logout();
    }
}
