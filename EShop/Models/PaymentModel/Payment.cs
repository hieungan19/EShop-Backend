﻿namespace EShop.Models.PaymentModel
{
    public class Payment
    {
        public string Id { get; set; } = string.Empty;
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; }
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchantId { get; set; } = string.Empty;
        public virtual Merchant? Merchant { get; set; } = null;
        public string? PaymentDestinationId { get; set; } = string.Empty;
        public virtual PaymentDestination? PaymentDestination { get; set; } = null; 
        public decimal? PaidAmount { get; set; }
        public string? PaymentStatus { get; set; } = string.Empty;
        public string? PaymentLastMessage { get; set; } = string.Empty;
    }
}
