using EShop.Data;
using EShop.DTOs.ReviewDTOs;
using EShop.Models.ReviewModel;

namespace EShop.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly EShopDBContext _context;


        public ReviewService(EShopDBContext context)
        {
            this._context = context;
           
        }
        public async Task<Review> Create(ReviewViewModel formData)
        {
            var review = new Review()
            {
                UserId = formData.UserId,
                Detail = formData.Detail,
                Star = formData.Star,
                ProductId = formData.ProductId, 
                CreatedDate = DateTime.Now,
            }; 

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

      
    }
}
