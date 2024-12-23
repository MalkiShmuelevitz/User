using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> Get(int position, int skip, string? desc, int? minPrice, int? maxPrice,
            int?[] categoryIds)
        {
            return await productRepository.Get(position, skip, desc, minPrice, maxPrice, categoryIds);
        }
       
    }
}
