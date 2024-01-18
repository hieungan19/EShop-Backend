using EShop.DTOs.ReviewDTOs;
using EShop.Models.ReviewModel;
using EShop.Services.ReviewServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService; 

        public ReviewController(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        [HttpPost]
        public async Task<Review> Create(ReviewViewModel formData)
        {

            var newReview = await _reviewService.Create(formData);
            return newReview;
        }
    }
}
