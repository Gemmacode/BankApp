using Core.Services;
using Core.Session;
using Data.Entities;
using Core.AppDashboard;
using Core.Implementations.Validations;
using Newtonsoft.Json;

namespace Core.Implementations
{
    public class Auth : IAuth
    {


       public static List<User> Users = new List<User>();



        public void SignUp()
        {
            Console.WriteLine("------------SIGNUP PORTAL------------");
            string FullName = AuthValidations.GetValidFullName();
            string Email = AuthValidations.GetValidEmail();
            string PassWord = AuthValidations.GetValidPassword();

            var createdUser = new User()
            {
                Id = Guid.NewGuid(),
                FullName = FullName,
                Email = Email,
                PassWord = PassWord,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            Help.SendCustomerDataToJsonFile(createdUser, "Customer.json");
            Console.Clear();
            Console.WriteLine("Registration Successful");  
            Login();
        }




        public User Login() 
        {
            Console.WriteLine("------------Login PORTAL------------");
            string Email = AuthValidations.GetValidEmail();
            string PassWord = AuthValidations.GetValidPassword();


            List<User> users = Help.ReadCustomersFromJsonFile("Customer.json");
            var UserExist = users.Find(Users => Users.Email == Email && Users.PassWord==PassWord);  
            if(UserExist != null)
            {
                Console.Clear();
                Console.WriteLine("Login successful!");
                UserSession.LoggedInUser = UserExist;
                var dash = new Dashboard();
                dash.MyDashBoard(UserSession.LoggedInUser);
            }

            else
            {
                Console.WriteLine("Invalid login");
                Login();
            }
            return UserExist;
        }





        public void Logout()
        {
            UserSession.LoggedInUser = null;
            Console.WriteLine("logged out");
        }





    }
}
