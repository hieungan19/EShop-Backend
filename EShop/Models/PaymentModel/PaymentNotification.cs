namespace EShop.Models.PaymentModel
{
    public class PaymentNotification
    {
        public string Id { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;

        public DateTime? NotiDate { get; set; }
        public string? NotiContent { get; set; } = string.Empty;
        public decimal NotiAmount { get; set; }
        public string? NotiMessage { get; set; } = string.Empty;
        public string? NotiSignature { get; set; } = string.Empty;
        public string? NotiPaymentId { get; set; } = string.Empty;
        public virtual Payment? Payment { get; set; }
        public string? NotiMerchantId { get; set; } = string.Empty;
        public virtual Merchant? Merchant { get; set; }
        public string? NotiNotiStatus { get; set; } = string.Empty;
        public DateTime? NotiResDate { get; set; }
        public string? NotiResMessage { get; set; } = string.Empty;
        public string? NotiResHttpCode { get; set; } = string.Empty;
    }
}
