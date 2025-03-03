using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get(int position, int skip, string? desc, int? minPrice, int? maxPrice,
            int?[] categoryIds);
        Task<Product> GetById(int id);

    }
}