namespace EShop.DTOs.ReviewDTOs
{
    public class ReviewViewModel
    {
        public int UserId { get; set; }
        public string Detail{ get; set; } = string.Empty;
        public double Star { get; set; }
        public int ProductId { get; set; }

    }
}
