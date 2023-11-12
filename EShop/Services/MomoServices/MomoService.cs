﻿using EShop.Data;
using EShop.Models.OrderModel;
using EShop.Models.PaymentModel;
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
            Order model =  _context.Orders.Where(o => o.Id == id).FirstOrDefault();
            model.Id = (int) DateTime.UtcNow.Ticks;
            model.OrderPaymentInfo = "Khách hàng: " + model.User.FullName + ". Nội dung: Thanh toán tại Lamut"; 
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.Id}&amount={model.TotalPrice}&orderId={model.Id}&orderInfo={model.OrderPaymentInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";

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
                orderId = model.Id,
                amount = model.TotalPrice.ToString(),
                orderInfo = model.OrderPaymentInfo,
                requestId = model.Id,
                extraData = "",
                signature = signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        }

        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
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
