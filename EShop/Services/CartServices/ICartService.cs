using EShop.DTOs.CartDTOs;

namespace EShop.Services.CartServices
{
    public interface ICartService
    {
        
            Task<LayoutCartViewModel> AddToCart(int userId, int productId, int quantity);
            Task<LayoutCartViewModel> RemoveFromCart(int userId, int optionId);
            Task<CartViewModel> GetUserCart(int userId);
            Task<UpdateCartViewModel> ChangeCartItemQuantity(int userId, int quantity, int optionId);


    }
}
