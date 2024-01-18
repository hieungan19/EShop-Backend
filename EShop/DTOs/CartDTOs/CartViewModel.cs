using EShop.DTOs.OptionDTOs;

namespace EShop.DTOs.CartDTOs
{
    public class CartViewModel
    {
        public List<OptionViewModel> Options { get; set; }
        public double TotalPrice { get; set; }
    }
}
