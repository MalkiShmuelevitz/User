using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> Post(Order order);
        Task<Order> GetById(int id);
    }
}