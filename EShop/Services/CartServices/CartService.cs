using EShop.Data;
using EShop.DTOs.CartDTOs;
using EShop.DTOs.OptionDTOs;
using EShop.Models.CartModel;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly EShopDBContext _context;

        public CartService(EShopDBContext context)
        {
            this._context = context;
        }
        public async Task<LayoutCartViewModel> AddToCart(int userId, int optionId, int quantity)
        {
            var user = this._context.Users.Where(u => u.Id == userId).Include(u => u.CartProductOptions).FirstOrDefault();

            var cartProduct = this._context.Carts.Where(c => c.UserId == userId && c.OptionId == optionId).FirstOrDefault();

            if (cartProduct != null)
            {
                cartProduct.Quantity += quantity;
            }
            else
            {
                var productOption = this._context.Options.Where(o => o.Id == optionId).FirstOrDefault();

                var cartProductToAdd = new Cart { UserId = user.Id, OptionId = productOption.Id, Quantity = quantity, UnitPrice = productOption.Price };

                user.CartProductOptions.Add(cartProductToAdd);
            }

            _context.SaveChanges();

            var model = new LayoutCartViewModel();

            model.OptionIds = user.CartProductOptions.Select(p => p.OptionId).ToList();
            model.Quantity = user.CartProductOptions.Select(p => p.Quantity).Sum();
            return model;
        }

        public async Task<CartViewModel> GetUserCart(int userId)
        {
            var user = this._context.Users.Where(u => u.Id == userId).Include(u => u.CartProductOptions).ThenInclude(c => c.Option).ThenInclude(o => o.Product).ThenInclude(p=>p.CurrentCoupon).FirstOrDefault();
            var cartProductOptions = user.CartProductOptions.ToList();
            var products = user.CartProductOptions.Select(c => c.Option).ToList();

            var optionsList = products.Select(o =>
            {
                o.Product.Category = null;
                o.Product.Options = null;
                OptionViewModel opt = new OptionViewModel();
                opt.Id = o.Id;
                opt.Name = o.Name;
                opt.Price = o.Price;
                opt.Quantity = cartProductOptions.Where(cpo => cpo.OptionId == o.Id).FirstOrDefault().Quantity;
                opt.ProductId = o.ProductId; 
                opt.ProductName = o.Product.Name;
                opt.ProductImageUrl = o.Product.ImageUrl; 

                if (o.Product.CurrentCoupon != null)
                {
                    if (o.Product.CurrentCoupon.DiscountAmount != null)
                    {
                        opt.CurrentPrice = opt.Price - o.Product.CurrentCoupon.DiscountAmount;
                    }
                    else
                    {
                        opt.CurrentPrice = opt.Price * (1 - o.Product.CurrentCoupon.DiscountPercent / 100);
                    }
                }

                return opt;
            }).ToList();

            var model = new CartViewModel();

            model.Options = optionsList;
            model.TotalPrice = optionsList.Select(o => o.Price * o.Quantity).Sum();

            return model;
        }

        public async Task<LayoutCartViewModel> RemoveFromCart(int userId, int optionId)
        {
            var user = this._context.Users.Where(u => u.Id == userId).Include(u => u.CartProductOptions).ThenInclude(c => c.Option).FirstOrDefault();

            var cartProductOption = this._context.Carts.Where(c => c.UserId == userId && c.OptionId == optionId).FirstOrDefault();
            this._context.Carts.Remove(cartProductOption);

            this._context.SaveChanges();

            var model = new LayoutCartViewModel();

            model.OptionIds = user.CartProductOptions.Select(o => o.OptionId).ToList();
            model.Quantity = user.CartProductOptions.Select(p => p.Quantity).Sum();
            return model;
        }
    }
}
