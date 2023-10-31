namespace EShop.Models.PaymentModel
{
    public class BaseAuditableEntity
    {
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string? LastUpdatedByy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
