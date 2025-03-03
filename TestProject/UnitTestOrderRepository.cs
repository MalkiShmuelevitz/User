using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UnitTestOrderRepository
    {
            [Fact]
            public async Task GetById_Validate_ReturnOrder_Unit()
            {
                // Arrange
                var orderId = 1;
                var user = new User { Id = 1, UserName = "testuser" };
                var orderItems = new List<OrderItem> { new OrderItem { Id = 1, OrderId = orderId, ProductId = 1 } };
                var order = new Order { Id = orderId, UserId = user.Id, User = user, OrderItems = orderItems };

                var mockContext = new Mock<ManagerDbContext>();
                mockContext.Setup(x => x.Orders).ReturnsDbSet(new List<Order> { order });

                var orderRepository = new OrderRepository(mockContext.Object);

                // Act
                var result = await orderRepository.GetById(orderId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(orderId, result.Id);
                Assert.Equal(user.Id, result.UserId);
                Assert.Equal(orderItems.Count, result.OrderItems.Count);
            }
    }
}
