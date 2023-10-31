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
            return await this._context.Orders.Select(o => new OrderViewModel()
            {
                Id = o.Id,
                UserId = o.UserId,
                MobilePhone = o.MobilePhone,
                ShippingAddress = o.ShippingAddress,
                TotalPrice = o.TotalPrice,
                Status = o.Status.GetType().GetMember(o.Status.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>().Name,
                OrderDate = o.OrderDate.ToString("dd/MM/yyyy HH:mm"),
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
                var product = productList.Where(p => p.Id == item.ProductId).Include(p => p.CurrentCoupon).FirstOrDefault();
                var coupon = product.CurrentCoupon; 
                double discountAmount = 0;
                
                if (DateTime.Now < coupon.EndDate && coupon.DiscountAmount != null)
                    discountAmount = coupon?.DiscountAmount ?? 0;
                else if (coupon.DiscountPercent!=null) discountAmount =  (coupon?.DiscountPercent??0) * item.Price/100;

                Console.Write(discountAmount); 

                orderItems.Add(new OrderItem()
                {
                    OptionId = item.Id ?? 0,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    DiscountAmount = discountAmount
                });
                total += item.Quantity * item.Price -  discountAmount * item.Quantity; 
            }
            if (formData.CouponId != null)
            {

                Coupon coupon = _context.Coupons.Where(c => c.Id == formData.CouponId).FirstOrDefault();
                Console.WriteLine(coupon.Name); 
                if (total>=coupon?.MinBillAmount )
                {
                    if (coupon?.DiscountAmount != null) orderDiscountAmount = coupon?.DiscountAmount ?? 0;
                    if (coupon?.DiscountPercent != null) orderDiscountAmount = total * (coupon?.DiscountPercent?? 0)/100;
                    if (coupon.MaxDiscountAmount != null && orderDiscountAmount > coupon.MaxDiscountAmount )
                    {
                        orderDiscountAmount = coupon.MaxDiscountAmount??0; 
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
                OrderDate = DateTime.Now,
                DiscountAmount = orderDiscountAmount
              
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
            var order = this._context.Orders.Where(o => o.Id == id).FirstOrDefault();
            var orderItemList = this._context.OrderItems.Where(oi => oi.OrderId == id).ToList();
            foreach(var i in orderItemList)
            {
                i.Order = null; 
            }
            var model = new OrderViewModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                MobilePhone = order.MobilePhone,
                OrderDate = order.OrderDate.ToString(),
                ShippingAddress = order.ShippingAddress,
                Status = order.Status.GetType().GetMember(order.Status.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>().Name,
                TotalPrice = order.TotalPrice,
                DiscountAmount = order.DiscountAmount,
                OrderItems  = orderItemList
                
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

                if (order.Status == OrderStatus.Shipped)
                {
                    foreach (var item in items)
                    {
                      
                        var opt = options!.Where(o => o.Id == item.OptionId).FirstOrDefault();

                        int quantity = opt.Quantity - item.Quantity;
                        Console.WriteLine(quantity); 

                        this._optionService.Update(new OptionViewModel() { Id = item.OptionId, Name = opt.Name, ProductId = opt.ProductId, Quantity = quantity });

                    }
                }
            }

            await this._context.SaveChangesAsync();
        }
    }
}
