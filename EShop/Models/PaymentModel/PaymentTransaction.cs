namespace EShop.Models.PaymentModel
{
    public class PaymentTransaction
    {
        public string Id { get; set; } = string.Empty;
        public string? TranMessage { get; set; } = string.Empty;
        public string? TranPayload { get; set; } = string.Empty;
        public string? TranStatus { get; set; } = string.Empty;
        public decimal? TranAmount { get; set; }
        public DateTime? TranDate { get; set; }
        public string? PaymentId { get; set; } = string.Empty;
        public virtual Payment Payment { get; set; }
        public string? TranRefId { get; set; } = string.Empty;
        
    }
}
