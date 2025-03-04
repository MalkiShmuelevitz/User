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
        private readonly DataBaseFixture _dbFixture;
        public IntegrationTestUserRepository()
        {
            _dbFixture = new();
        }

        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
            var _repository = new UserRepository(_dbFixture.Context);

            // Act
            var user = new User { FirstName = "Malki", LastName = "Shmuelevitz", UserName = "mmm@gmail.com", Password = "21436@dfgAS@@!" };
            var dbUser = await _repository.Post(user);

            // Assert
            Assert.NotNull(dbUser);
            Assert.NotEqual(0, dbUser.Id);
            Assert.Equal("mmm@gmail.com", dbUser.UserName);
            _dbFixture.Dispose();
        }

    }
}
