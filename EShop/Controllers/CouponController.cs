using EShop.DTOs.CouponDTOs;
using EShop.Models.CouponModel;
using EShop.Services.CouponServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponController : ControllerBase
    {

        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            this._couponService = couponService;
        }
        [HttpPost]
        public async Task<Coupon> Create(CouponViewModel formData)
        {

            var newCoupon = await  _couponService.Create(formData);
            return newCoupon; 
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _couponService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
        
            return Ok(_couponService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCouponById(int id)
        {

            return Ok(_couponService.GetCouponById(id));
        }
        [HttpPut("{id}")]
        public async Task UpdateCoupon(int id, CouponViewModel newCoupon)
        {
            _couponService.Update(id, newCoupon);
        }
    }
}
