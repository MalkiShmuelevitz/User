using Entities;
using Microsoft.EntityFrameworkCore;
using Store.Models;
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

        public async Task<List<Product>> Get()
        {
            return await _managerDbContext.Products.ToListAsync();
        }

       
    }
}
