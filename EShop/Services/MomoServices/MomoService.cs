using EShop.Data;
using EShop.Models.OrderModel;
using EShop.Models.PaymentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace EShop.Services.MomoServices
{
    public class MomoService:IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;
        private readonly EShopDBContext _context;

        public MomoService(EShopDBContext context, IOptions<MomoOptionModel> options)
        {
            _options = options;
            this._context = context;
        }
       
        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync( int id)
        {
            Order model =  _context.Orders.Where(o => o.Id == id).Include(o=>o.User).FirstOrDefault();
            string orderPaymentInfo = "Khách hàng: " + model.User.FullName + ". Nội dung: Thanh toán tại Lamut";
            model.Id += 123456;
            var total = model.DiscountAmount>0 ? model.TotalPrice - model.DiscountAmount : model.TotalPrice;
            Console.WriteLine(total); 
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.Id}&amount={total}&orderId={model.Id}&orderInfo={orderPaymentInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";
            Console.WriteLine(rawData); 
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.Id.ToString(),
                amount = (model.TotalPrice - model.DiscountAmount).ToString(),
                orderInfo = orderPaymentInfo,
                requestId = model.Id.ToString(),
                extraData = "",
                signature = signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            Console.Write(response.Content); 

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        }

        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            Order order = _context.Orders.Where(o=>o.Id==(int.Parse(orderId)-123456)).FirstOrDefault();
            order.IsPayed = true;
            Console.WriteLine(order.IsPayed); 
            this._context.Update(order);
            _context.SaveChangesAsync();

            return new MomoExecuteResponseModel()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo
            };
            
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}
