using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ManagerDbContext manageDbContext;

        public CategoryRepository(ManagerDbContext manageDbContext)
        {
            this.manageDbContext = manageDbContext;
        }


        public async Task<List<Category>> Get()
        {
            return await manageDbContext.Categories.ToListAsync();
        }
        
    }
}
