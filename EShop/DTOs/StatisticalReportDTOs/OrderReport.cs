using EShop.Models.OrderModel;

namespace EShop.DTOs.StatisticalReportDTOs
{
    public class OrderReport
    {
        public int Quantity { get; set; }
        public double Revenue { get; set; }
        public Dictionary<OrderStatus, int> StatusCounts { get; set; } = new Dictionary<OrderStatus, int>(); 

    }
}
