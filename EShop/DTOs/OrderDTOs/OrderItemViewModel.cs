namespace EShop.DTOs.OrderDTOs
{
    public class OrderItemViewModel
    {

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public int ProductId { get; set; }
        public double? DiscountAmount { get; set; }
       
    }
}
