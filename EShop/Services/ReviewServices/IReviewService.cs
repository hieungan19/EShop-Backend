﻿using EShop.DTOs.ReviewDTOs;
using EShop.Models.ReviewModel;

namespace EShop.Services.ReviewServices
{
    public interface IReviewService
    {
        public Task<Review> Create(ReviewViewModel formData);
    }
}
