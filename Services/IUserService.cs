using Entities;

namespace Services
{
    public interface IUserService
    {
        int CheckPassword(string password);
        Task<User> Post(User user);
        Task<User> PostLoginS(string username, string password);
        void Put(int id, User user);
    }
}