using EShop.DTOs.ProductDTOs;
using EShop.DTOs.StatisticalReportDTOs;
using EShop.Models.Products;

namespace EShop.Services.StatisticalReport
{
    public interface IStatisticalReport 
    {

        // Tính số lượng khách hàng theo từng cấp độ [0,<1tr, 1-5tr, 5-10tr, >10tr] (theo số tiền khách đã mua) 
         CustomerReport CalculateNumberOfCustomersByLevel();

        // Tính số lượng phân loại, sản phẩm thuộc phân loại. 
          Dictionary<string, int> CountProductsByCategory();
        // Tính số lượng, doanh thu,  các đơn hàng và trạng thái từng đơn hàng. 
        OrderReport CalculateOrderStatistics();
        // Sắp xếp các sản phẩm bán chạy 
       ProductListViewModel SortProductsByQuantitySold();
        // Sắp xếp các sản phẩm có lượt review cao
        ProductListViewModel SortProductsByAverageReview();
        Task<List<MonthlyStatistic>> GetMonthlyStatisticsAsync(int year);




    }
}
