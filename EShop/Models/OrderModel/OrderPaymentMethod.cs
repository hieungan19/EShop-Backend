using System.ComponentModel.DataAnnotations;

namespace EShop.Models.OrderModel
{
    public enum OrderPaymentMethod
    {

        [Display(Name = "COD")]
        COD,
        [Display(Name = "Momo")]
        Momo,
        
    }
}
