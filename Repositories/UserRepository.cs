using Entities;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ManagerDbContext _managerDbContext;

        public UserRepository(ManagerDbContext managerDbContext)
        {
            _managerDbContext = managerDbContext;
        }
        public async Task<User> GetById(int id)
        {
            User user = await _managerDbContext.Users.FirstOrDefaultAsync(u => u.Id==id);
            return user;
        }
        public async Task<User> PostLoginR(string username, string password)
        {
            User user = await _managerDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            return user;
        }
        public async Task<User> Post(User user)
        {
            await _managerDbContext.Users.AddAsync(user);
            await _managerDbContext.SaveChangesAsync();
            return user;
        }
        public async Task Put(int id,User user1)
        {
            user1.Id = id;
            _managerDbContext.Users.Update(user1);
            await _managerDbContext.SaveChangesAsync();
        }
    }
}
