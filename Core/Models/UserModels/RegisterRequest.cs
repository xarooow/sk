using sk.Core.Exceptions;
using sk.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace sk.Core.Models.UserModels
{
    public class RegisterRequest
    {
        public string Login;
        public string EmailOrPhoneNumber;
        public string Password;

        public RegisterRequest(string login, string emailOrPhoneNumber, string password)
        {
            if(!(login=="" && emailOrPhoneNumber=="" && password==""))
            {
                throw new RegLogException();
            }
            Login = login;
            EmailOrPhoneNumber = emailOrPhoneNumber;
            Password = password;
        }
    }
}
