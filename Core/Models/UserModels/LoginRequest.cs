using sk.Core.Exceptions;
using sk.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace sk.Core.Models.UserModels
{
    public class LoginRequest: IModel
    {
        [Required]
        public string Login;
        [Required]
        public string Password;

        public LoginRequest(string login, string password)
        {
            if (!(login == "" && password == ""))
            {
                throw new RegLogException();
            }
            Login = login;
            Password = password;
        }
    }
}
