using sk.Core.Interfaces;
using sk.Core.Models.UserModels;

namespace sk.Infrastructure
{
    public class MockUserDB: IDataBase<User>
    {
        private readonly List<User> _users = new List<User>();
        public async Task<bool> IsExist(User user)
        {
            return _users.Any(x => x.Id == user.Id);
        }

        public bool IsExist(LoginRequest request)
        {
            return true;
        }

        public async Task Save(User user)
        {
            if (user.Id == 0 || user.Id == null)
            {
                user.Id = _users.Any() ? _users.Max(e => e.Id) + 1 : 1;
            }

            _users.Add(user);

            return;
        }

        public async Task Delete(User user)
        {
            _users.Remove(user);
        }

        public async Task<List<User>> GetList(int quant = default)
        {
            if (! (quant == default))
            {
                return _users.Take(quant).ToList();
            }
            else { return _users.ToList(); }
        }
    }
}
