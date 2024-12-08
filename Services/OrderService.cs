using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Order> Post(Order order)
        {
            return await orderRepository.Post(order);
        }
        public async Task<Order> GetById(int id)
        {
            return await orderRepository.GetById(id);
        }
    }
}
