using Entities;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UserService : IUserService
    {

        IUserRepository _iUserRepository;

        public UserService(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }

        public User PostLoginS(string username, string password)
        {
            return _iUserRepository.PostLoginR(username, password);
        }
        public User Post(User user)
        {            //check password strength

            return _iUserRepository.Post(user);
        }

        public void Put(int id, User user)
        {            //check password strength

            _iUserRepository.Put(id, user);
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return (result.Score);

        }
    }
}
