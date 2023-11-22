using EShop.DTOs.CouponDTOs;
using EShop.Models.CouponModel;

namespace EShop.Services.CouponServices
{
    public interface ICouponService
    {
        public Task<Coupon> Create(CouponViewModel coupon ); 
        public Task<Coupon> Update(int id,CouponViewModel newCoupon);
        public Task Delete(int id);
        public Task<Coupon> GetCouponById(int id); 
        public Task<CouponListViewModel> GetAll();

    }
}
