using AutoMapper;
using Entities;
using DTO;

namespace Store
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Order, GetOrderDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, GetUserDTO>();
        }
    }
}
