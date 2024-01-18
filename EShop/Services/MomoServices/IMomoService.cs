using EShop.Models.OrderModel;
using EShop.Models.PaymentModel;

namespace EShop.Services.MomoServices
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(int id);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
