using Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ManagerDbContext _managerDbContext;

        public ProductRepository(ManagerDbContext managerDbContext)
        {
            _managerDbContext = managerDbContext;
        }

        public async Task<List<Product>> Get(int position,int skip,string? desc, int? minPrice,int? maxPrice,
            int?[] categoryIds)
        {
            var query =  _managerDbContext.Products.Where(product =>
            (desc == null ? (true) : (product.Description.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
            //.Skip((position - 1) * skip)
            //.Take(skip);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Include(p => p.Category).ToListAsync();
            //return await _managerDbContext.Products.Include(p=>p.Category).ToListAsync();
            return products;
        }
        public async Task<Product> GetById(int id)
        {
            return await _managerDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        }

    }
}
