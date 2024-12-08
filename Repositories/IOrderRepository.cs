using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Post(Order order);
        Task<Order> GetById(int id);

    }
}