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
        public async Task<User> GetById(int id)
        {
            User user = await _iUserRepository.GetById(id);
            return user;
        }
        public async Task<User> PostLoginS(string username, string password)
        {
            return await _iUserRepository.PostLoginR(username, password);
        }
        public async Task<User> Post(User user)
        {
            int result = CheckPassword(user.Password);
            if (result <= 3)
                return null;
            await _iUserRepository.Post(user);
            return user;
        }

        public async Task Put(int id, User user)
        {
           await _iUserRepository.Put(id, user);
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return (result.Score);

        }
    }
}
