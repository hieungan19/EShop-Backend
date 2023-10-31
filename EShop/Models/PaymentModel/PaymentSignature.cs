﻿namespace EShop.Models.PaymentModel
{
    public class PaymentSignature
    {
        public string Id { get; set; } = string.Empty;
        public string? PaymentId { get; set; } = string.Empty;
        public virtual Payment Payment { get; set; }
        public string? SignValue { get; set; } = string.Empty;
        public string? SignAlgo { get; set; } = string.Empty;
        public string? SignOwn { get; set; } = string.Empty;
        public DateTime? SignDate { get; set; }
        public bool IsValid { get; set; }
    }
}
