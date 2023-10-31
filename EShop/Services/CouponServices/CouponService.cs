using EShop.Data;
using EShop.DTOs.CouponDTOs;
using EShop.Models.CouponModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EShop.Services.CouponServices
{
    public class CouponService : ICouponService
    {
        private readonly EShopDBContext _context;
        public CouponService(EShopDBContext context)
        {
            this._context = context;
        }
        public async Task<Coupon> Create(CouponViewModel formData)
        {
            var coupon = new Coupon()
            {
                Name = formData.Name,
                Desciption = formData.Desciption,
                DiscountAmount = formData.DiscountAmount,
                DiscountPercent = formData.DiscountPercent,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                
            };
            Enum.TryParse<ApplyCouponType>(formData.ApplyCouponType, out ApplyCouponType type); 

            coupon.ApplyCouponType = type;
            if (type == ApplyCouponType.Order)
            {
                coupon.MaxDiscountAmount = formData.MaxDiscountAmount;
                coupon.MinBillAmount = formData.MinBillAmount;

            }

            _context.Coupons.Add(coupon);
            _context.SaveChanges();
            return coupon;
        }

        public async Task Delete(int id)
        {

            var coupon = _context.Coupons.FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                throw new Exception();
            }

            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
        }

        public async Task<IQueryable<Coupon>> GetAll()
        {

            var query = _context.Coupons.AsQueryable();

            return query; 
            
        }

        public async Task<Coupon> GetCouponById(int id)
        {
            var coupon = _context.Coupons.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                throw new Exception();
            }
            return coupon; 
        }

        public async Task<Coupon> Update(int id, CouponViewModel newCoupon)
        {
            
            var coupon = _context.Coupons.FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                throw new Exception();
            }
            if (newCoupon.Name != null)
            {
                coupon.Name = newCoupon.Name;
            }
            if (newCoupon.Desciption != null)
            {
                coupon.Desciption = newCoupon.Desciption;
            }
            if (newCoupon.StartDate != null)
            {
                coupon.StartDate = newCoupon.StartDate; 
            }
            if (newCoupon.EndDate != null)
            {
                coupon.EndDate = newCoupon.EndDate;
            }
            if (newCoupon.DiscountAmount != null)
            {
                coupon.DiscountAmount = newCoupon.DiscountAmount;
            }
            if (newCoupon.DiscountPercent != null)
            {
                coupon.DiscountPercent = newCoupon.DiscountPercent;
            }
            if (newCoupon.MaxDiscountAmount != null)
            {
                coupon.MaxDiscountAmount = newCoupon.MaxDiscountAmount;
            }
            if (newCoupon.MinBillAmount != null)
            {
                coupon.MinBillAmount = newCoupon.MinBillAmount;
            }
            if (newCoupon.ApplyCouponType != null)
            {
                
                Enum.TryParse<ApplyCouponType>(newCoupon.ApplyCouponType, out ApplyCouponType cType); 
                coupon.ApplyCouponType = cType; 
            }


            this._context.Update(coupon); 
            return coupon;
            
        }

        
    }
}
