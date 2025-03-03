using Entities;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class IntegrationTestUserRepository : IClassFixture<DataBaseFixture>
    {

        private readonly ManagerDbContext _context;

        public IntegrationTestUserRepository(DataBaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetUserLogin_validate_returnUser()
        {
            // Arrange
            var user = new User { UserName = "test@example.com", Password = "password123" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var retrievedUser = await _context.Users.FindAsync(user.Id);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserName, retrievedUser.UserName);
            Assert.Equal(user.Password, retrievedUser.Password);

        }
    }
}
