using Entities;
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
    public class UnitTestCategoryRepository
    {
        [Fact]
        public async Task Get_Should_Return_All_Categories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Category1" },
                new Category { Id = 2, CategoryName = "Category2" }
            };

            var mockContext = new Mock<ManagerDbContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Category1", result[0].CategoryName);
            Assert.Equal("Category2", result[1].CategoryName);
        }
    }
}
