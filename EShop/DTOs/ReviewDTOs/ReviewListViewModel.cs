namespace EShop.DTOs.ReviewDTOs
{
    public class ReviewListViewModel
    {


        public ReviewListViewModel()
        {
            this.Reviews = new List<ReviewViewModel>();
        }

        public List<ReviewViewModel> Reviews { get; set; }

    }


}