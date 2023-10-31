namespace EShop.Models.PaymentModel
{
    public class PaymentDestination
    {
        public string Id { get; set; } = string.Empty;
        public string? DesName { get; set; } = string.Empty;
        public string? DesShortName { get; set; } = string.Empty;
        public string? DesParentId { get; set; } = string.Empty;
        public virtual PaymentDestination DesParent { get; set; }   
        public string? DesLogo { get; set; } = string.Empty;
        public int SortIndex { get; set; }
        public bool IsActive { get; set; }
    }
}
