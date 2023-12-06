using EShop.DTOs.ProductDTOs;
using EShop.DTOs.StatisticalReportDTOs;
using EShop.Services.StatisticalReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class StatisticalReportController : ControllerBase
    {
        private readonly IStatisticalReport _statisticalService;

        public StatisticalReportController(IStatisticalReport statisticalService)
        {
            this._statisticalService = statisticalService;
        }

        [HttpGet("customer")]
        public CustomerReport  GetCustomerReport()
        {
            return _statisticalService.CalculateNumberOfCustomersByLevel();
        }

        [HttpGet("order")]
        public OrderReport GetOrderReport()
        {
            return _statisticalService.CalculateOrderStatistics(); 
        }

        [HttpGet("category")]
        public Dictionary<string, int> GetProductsByCategories()
        {
            return  _statisticalService.CountProductsByCategory(); 
        }

        [HttpGet("product-review")]
        public ProductListViewModel SortProductsByAverageReview()
        {
            return _statisticalService.SortProductsByAverageReview();
        }

        [HttpGet("product-sold")]
        public ProductListViewModel SortProductsByQuantitySold()
        {
            return _statisticalService.SortProductsByQuantitySold();
        }

        [HttpGet("monthly-statistics")]
        public async Task<IActionResult> GetMonthlyStatistics()
        {
            // Assuming you want data for the year 2023
            var monthlyStatistics = await _statisticalService.GetMonthlyStatisticsAsync(2023);
            return Ok(monthlyStatistics);
        }
    }
}
