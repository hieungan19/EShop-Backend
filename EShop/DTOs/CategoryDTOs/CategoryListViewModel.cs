namespace EShop.DTOs.CategoryDTOs
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public List<CategoryViewModel> Categories { get; set; }
    }
}
