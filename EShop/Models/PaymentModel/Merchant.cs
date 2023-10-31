namespace EShop.Models.PaymentModel
{
    public class Merchant : BaseAuditableEntity
    {
        public string Id { get; set; } = string.Empty;
        public string? MerchantName { get; set; } = string.Empty;
        public string? MerchantWebLink { get; set; } = string.Empty;
        public string? MerchantIpnUrl { get; set; } = string.Empty;
        public string? MerchantReturnUrl { get; set; } = string.Empty;
        public string? SecretKey { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<PaymentNotification> paymentNotifications { get; set; }  
    }
}
