using EShop.DTOs.OrderDTOs;
using EShop.Models.OrderModel;

namespace EShop.Services.OrderServices
{
    public interface IOrderService
    {
        Task <Order> Create(OrderViewModel formData);
        Task UpdateOrderStatus(int orderId, OrderStatus status);
        OrderViewModel GetOrderById(int id);
        Task<List<OrderViewModel>> GetAllOrders();
    }
}
