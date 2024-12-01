using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> Post(User user);
        Task<User> PostLoginR(string username, string password);
        Task Put(int id, User user1);
    }
}