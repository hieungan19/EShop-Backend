namespace EShop.DTOs.CartDTOs
{
    public class LayoutCartViewModel
    {
        public LayoutCartViewModel()
        {
            this.OptionIds = new List<int>();
        }

        public List<int> OptionIds { get; set; }
        public int Quantity { get; set; }
    }
}
