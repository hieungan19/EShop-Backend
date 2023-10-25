﻿using EShop.DTOs.CartDTOs;

namespace EShop.Services.CartServices
{
    public interface ICartService
    {
        
            Task<LayoutCartViewModel> AddToCart(int userId, int productId, int quantity);
            Task<LayoutCartViewModel> RemoveFromCart(int userId, int productId);
            Task<CartViewModel> GetUserCart(int userId);
        
    }
}