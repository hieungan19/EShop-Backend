using EShop.DTOs.OrderDTOs;
using EShop.Models.OrderModel;
using EShop.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class CurrentStatus
    {
        public string Status { get; set; }
    }

    [Route("api/orders")]
    [ApiController]
   
    public class OrderController: ControllerBase 
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel formData)
        {
            var order = await this._orderService.Create(formData);
            return Ok(order); 
        }

        [HttpGet]
        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            return await this._orderService.GetAllOrders();
        }

        [HttpGet("{id}")]
        public OrderViewModel GetOrder(int id)
        {
            return this._orderService.GetOrderById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] CurrentStatus OrderStatus)
        {
        
            if (Enum.TryParse<OrderStatus>(OrderStatus.Status, out OrderStatus orderStatus))
            {
                await this._orderService.UpdateOrderStatus(id, orderStatus);
            }
           
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<List<OrderViewModel>> GetAllOrdersByUserId( int userId)
        {
            
            return await this._orderService.GetAllOrdersByUserId(userId);

        }
    }
}
