using EShop.DTOs.CartDTOs;
using EShop.Services.CartServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Controllers
{

    [Route("api/carts")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpPost]
        public async Task<LayoutCartViewModel> AddToCart(UpdateCartViewModel model)
        {
            return await this._cartService.AddToCart(model.UserId, model.OptionId, model.Quantity);
        }

        [HttpDelete]
        public async Task<LayoutCartViewModel> RemoveFromCart([FromQuery] UpdateCardViewModel model)
        {
            return await this._cartService.RemoveFromCart(model.UserId, model.OptionId);
        }

        [HttpGet("{id}")]
        public async Task<CartViewModel> GetUserCart(int id)
        {
            return await this._cartService.GetUserCart(id);
        }
    }
}
