using System.ComponentModel.DataAnnotations;

namespace EShop.Models.Shipment
{
    public enum ShipmentStatus
    {

        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Shipped")]
        Shipped,
        [Display(Name = "Completed")]
        Finished,
        [Display(Name = "Cancelled")]
        Cancelled

    }
}
