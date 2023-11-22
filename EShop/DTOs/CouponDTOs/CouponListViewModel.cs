namespace EShop.DTOs.CouponDTOs
{
    public class CouponListViewModel
    {
        public CouponListViewModel()
        {
            this.Coupons = new List<CouponViewModel>();
        }

        public List<CouponViewModel> Coupons { get; set; }
    }
}
