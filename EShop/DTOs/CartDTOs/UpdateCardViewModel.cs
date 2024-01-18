namespace EShop.DTOs.CartDTOs
{
    public class UpdateCartViewModel
    {
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public int? Quantity { get; set; }
    }
}
