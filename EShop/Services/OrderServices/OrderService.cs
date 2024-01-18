using EShop.Data;
using EShop.DTOs.OptionDTOs;
using EShop.DTOs.OrderDTOs;
using EShop.Models.CouponModel;
using EShop.Models.OrderModel;
using EShop.Models.Products;
using EShop.Services.CartServices;
using EShop.Services.OptionServices;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EShop.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly EShopDBContext _context;
        private readonly ICartService _cartService;
        private readonly IOptionService _optionService;
        public OrderService(EShopDBContext context, ICartService cartService, IOptionService optionService)
        {
            this._context = context;
            this._cartService = cartService;
            this._optionService = optionService;
        }


        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            return await this._context.Orders.Include(o=>o.User).OrderByDescending(o => o.OrderDate).Select(o => new OrderViewModel()
            {
                Id = o.Id,
                UserId = o.UserId,
                MobilePhone = o.MobilePhone,
                ShippingAddress = o.ShippingAddress,
                DiscountAmount = o.DiscountAmount, 
                TotalPrice = o.TotalPrice,
                IsPayed = o.IsPayed,
                Status = o.Status,
                PaymentMethod = o.PaymentMethod, 
                ReceiverName = o.ReceiverName, 
                OrderDate = o.OrderDate,
                
            }).ToListAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersByUserId(int userId)
            
        {
            Console.WriteLine(userId);
            return await this._context.Orders.Where(o=>o.UserId==userId).Include(o => o.User).OrderByDescending(o => o.OrderDate).Select(o => new OrderViewModel()
            {
                Id = o.Id,
                UserId = o.UserId,
                MobilePhone = o.MobilePhone,
                ShippingAddress = o.ShippingAddress,
                TotalPrice = o.TotalPrice,
                DiscountAmount = o.DiscountAmount, 
                IsPayed = o.IsPayed,
                Status = o.Status,
                PaymentMethod = o.PaymentMethod,
                ReceiverName = o.ReceiverName,
                OrderDate = o.OrderDate,

            }).ToListAsync();
        }
        public async Task<Order> Create(OrderViewModel formData)
        {

            //var cart = this._context.Carts.Where(c => c.UserId == formData.UserId).Include(c => c.OptionId).ToList();
            var orderItems = new List<OrderItem>();
            var itemsList = formData.ItemsList;
            var productList = _context.Products;
            double total = 0;
            double orderDiscountAmount = 0;
            
            foreach (var item in itemsList)
            {
                Option option = _context.Options.Where(o => o.Id == item.Id).Include(o=>o.Product).ThenInclude(p=>p.CurrentCoupon).FirstOrDefault();
                double price = option.Price;
                Product product = option.Product;
                
                var coupon = product.CurrentCoupon;

                double discountAmount = 0;
                if (coupon != null)
                {
                    if (DateTime.Now < coupon.EndDate && coupon.DiscountAmount != null)
                        discountAmount = coupon?.DiscountAmount ?? 0;
                    else if (coupon.DiscountPercent != null) discountAmount = (coupon?.DiscountPercent ?? 0) * price / 100;
                }

                Console.Write(discountAmount);

                orderItems.Add(new OrderItem()
                {
                    OptionId = item.Id ?? 0,
                    Quantity = item.Quantity,
                    UnitPrice = price,
                    DiscountAmount = discountAmount
                });
                total += item.Quantity * price - discountAmount * item.Quantity;
            }
            if (formData.CouponId != null)
            {

                Coupon coupon = _context.Coupons.Where(c => c.Id == formData.CouponId).FirstOrDefault();
                Console.WriteLine(coupon.Name);
                if (total >= coupon?.MinBillAmount || coupon?.MinBillAmount == null || coupon?.MinBillAmount == 0)
                {
                    if (coupon?.DiscountAmount != null) orderDiscountAmount = coupon?.DiscountAmount ?? 0;
                    if (coupon?.DiscountPercent != null) orderDiscountAmount = total * (coupon?.DiscountPercent ?? 0) / 100;
                    if (coupon?.MaxDiscountAmount != null && orderDiscountAmount > coupon.MaxDiscountAmount)
                    {
                        orderDiscountAmount = coupon.MaxDiscountAmount ?? 0;
                    }


                }


            }

            var order = new Order()
            {
                OrderItems = orderItems,
                Status = OrderStatus.Pending,
                TotalPrice = total,
                ShippingAddress = formData.ShippingAddress,
                MobilePhone = formData.MobilePhone,
                UserId = formData.UserId,
                ReceiverName = formData.ReceiverName, 
                OrderDate = DateTime.Now,
                DiscountAmount = orderDiscountAmount,
                PaymentMethod = formData.PaymentMethod,
                CouponId = formData.CouponId,

            };


            this._context.Orders.Add(order);

            foreach (var item in itemsList)
            {
                _cartService.RemoveFromCart(formData.UserId, item.Id ?? 0);
            }

            this._context.SaveChanges();
            return order;
        }

        public OrderViewModel GetOrderById(int id)
        {
            var order = this._context.Orders.Where(o => o.Id == id).Include(o=>o.User).FirstOrDefault();
            var orderItemList = this._context.OrderItems.Where(oi => oi.OrderId == id).Include(oi=>oi.ProductOption).ThenInclude(o=>o.Product).ToList();
            foreach (var i in orderItemList)
            {
                i.Order = null;
            }
            var model = new OrderViewModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                MobilePhone = order.MobilePhone,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                DiscountAmount = order.DiscountAmount,
                OrderItems = orderItemList.Select(oi => new OrderItemViewModel { OrderId = oi.OrderId, OptionId = oi.OptionId, Name= oi.ProductOption.Name, ProductName = oi.ProductOption.Product.Name, ProductImageUrl = oi.ProductOption.Product.ImageUrl, ProductId = oi.ProductOption.ProductId, Quantity = oi.Quantity, DiscountAmount = oi.DiscountAmount, UnitPrice= oi.UnitPrice }).ToList(),
                IsPayed = order.IsPayed,
                UserInfo = new DTOs.Account.UserViewModel() { Id = order.UserId, FullName=order.User.FullName,AvatarUrl = order.User.AvatarUrl, Email = order.User.Email }, 
                ReceiverName = order.ReceiverName,
                CouponId = order.CouponId
            };

            return model;
        }

        public async Task UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = this._context.Orders.Where(o => o.Id == orderId).Include(o => o.OrderItems).FirstOrDefault();
            var options = this._context.Options; // product options
            var items = order!.OrderItems;  //item in order
            if (order.Status != status)
            {
                order.Status = status;
                this._context.Update(order);
                Console.WriteLine(order.Status); 
                if (order.Status == OrderStatus.Shipped)
                {
                    foreach (var item in items)
                    {

                        var opt = options!.Where(o => o.Id == item.OptionId).FirstOrDefault();
                        var product = _context.Products.Where(p => p.Id == opt.ProductId).FirstOrDefault();
                        product.QuantitySold = product.QuantitySold + item.Quantity;
                        int quantity = opt.Quantity - item.Quantity;
                        Console.WriteLine(opt.Name); 
                        Console.WriteLine(quantity); 
                        this._optionService.Update(new OptionViewModel() { Id = item.OptionId, Name = opt.Name, ProductId = opt.ProductId, Quantity = quantity });

                    }
                }

                if (order.Status == OrderStatus.Finished)
                {

                    order.IsPayed = true;
                    this._context.Update(order);
                }
            }

            await this._context.SaveChangesAsync();
        }
    }
}
