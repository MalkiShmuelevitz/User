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
        IOrderRepository _orderRepository;
        IProductRepository _productRepository;


        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> Post(Order order)
        {
            bool check = await CheckSumOfOrder(order);
            if (!check)
                return null;

            Order order1 = await _orderRepository.Post(order);
            return order1;
        }
        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }
        public async Task<bool> CheckSumOfOrder(Order order)
        {
            int sum = 0;
            foreach (var item in order.OrderItems)
            {
               Product p = await _productRepository.GetById(item.ProductId);
               sum += p.Price;
            }
            return (sum == order.OrderSum);
        }

    }
}
