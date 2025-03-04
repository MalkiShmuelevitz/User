using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IMapper _imapper;
        private readonly ILogger<OrdersController> _logger;


        public OrdersController(IOrderService orderService,IMapper _imapper, ILogger<OrdersController> logger)
        {
            this._imapper = _imapper;
            this._orderService = orderService;
            _logger = logger;
        }

        ////GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTO>> Get(int id)
        {
            Order order = await _orderService.GetById(id);
            GetOrderDTO orderDTO = _imapper.Map<Order, GetOrderDTO>(order);
            if (orderDTO == null)
                return NoContent();
            return Ok(orderDTO);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<GetOrderDTO>> Post([FromBody] OrderDTO order)
        {
            if (order.OrderItems.Count == 0)
                return NoContent();
            Order orderF = _imapper.Map<OrderDTO, Order>(order);
            Order order1 = await _orderService.Post(orderF);
            if(order1 == null)
            {
                _logger.LogCritical($" User Id: {order.UserId} try to change the SUM of the order");
                return Unauthorized();
            }
            //return CreatedAtAction(nameof(GetById), new { Id = order.Id }, order);
            GetOrderDTO getOrder = _imapper.Map<Order, GetOrderDTO>(order1);
            return Ok(getOrder);
            //////////////////////////////////????
        }

    }
}
