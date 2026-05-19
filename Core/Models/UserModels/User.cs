using sk.Core.Interfaces;

namespace sk.Core.Models.UserModels
{
    public class User: IModel
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public User(int id, string login, string password, string? name, string? email, string? phoneNumber)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
