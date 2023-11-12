using EShop.Models.OrderModel;
using EShop.Services.MomoServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace EShop.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;
        public PaymentController(IMomoService momoService)
        {
            this._momoService =momoService ;
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> CreatePaymentUrl(int id)
        {

            var response = await _momoService.CreatePaymentAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return Ok(response);
        }
    }
}
