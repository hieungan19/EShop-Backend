namespace EShop.DTOs
{
    public class FilterViewModel
    {

        //int curPage = 0, int categoryId = 0, decimal priceFrom = 0, decimal priceTo = 0, int order = 0, int perPage
        public int CurrentPage { get; set; }
        public int CategoryId { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public int Order { get; set; }
        public int PerPage { get; set; }



    }
}
