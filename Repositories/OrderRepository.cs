using Entities;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ManagerDbContext managerDbContext;

        public OrderRepository(ManagerDbContext managerDbContext)
        {
            this.managerDbContext = managerDbContext;
        }
        public async Task<Order> GetById(int id)
        {
            return await managerDbContext.Orders.FindAsync(id);
            //return await _managerDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        }
        public async Task<Order> Post(Order order)
        {
            await managerDbContext.Orders.AddAsync(order);
            await managerDbContext.SaveChangesAsync();
            return order;
        }
    }
}
