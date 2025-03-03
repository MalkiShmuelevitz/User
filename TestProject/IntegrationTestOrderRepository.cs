using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class IntegrationTestOrderRepository : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _dbFixture;
        public IntegrationTestOrderRepository()
        {
            _dbFixture = new();
        }

        [Fact]
        public async Task CreateOrder_Should_Add_Order_To_Database()
        {
            // Arrange
            var _repository = new OrderRepository(_dbFixture.Context);

            // Act
            var order = new Order {Id = 1, OrderDate = DateTime.Now, OrderSum = 0, UserId =  1 };
            var dbOrder = await _repository.Post(order);

            // Assert
            Assert.NotNull(dbOrder);
            Assert.NotEqual(0, dbOrder.Id);
            Assert.Equal(1, dbOrder.UserId);
        }
    }
}
