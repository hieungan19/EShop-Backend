using EShop.Data;
using EShop.DTOs.ProductDTOs;
using EShop.DTOs.StatisticalReportDTOs;
using EShop.Models.Account;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace EShop.Services.StatisticalReport
{
    public class StatisticalReport : IStatisticalReport
    {
        private readonly EShopDBContext _context;



        public StatisticalReport(EShopDBContext context)
        {
            this._context = context;

        }
        public async Task<List<MonthlyStatistic>> GetMonthlyStatisticsAsync(int year)
        {
            var allMonths = Enumerable.Range(1, 12); // Generate a sequence of all months (1 to 12)

            var monthlyStatistics = (
                from month in allMonths
                join order in _context.Orders
                    .Where(o => o.IsPayed && o.OrderDate != null && o.OrderDate.Year == year)
                    on month equals order.OrderDate.Month into g
                from orderGroup in g.DefaultIfEmpty() // Left join
                group orderGroup by month into grouped
                select new MonthlyStatistic
                {
                    Month = grouped.Key,
                    NumberOfOrders = grouped.Count(o => o != null), // Count non-null orders
                    TotalRevenue = grouped.Sum(o => (o?.TotalPrice - o?.DiscountAmount) ?? 0) // Sum revenue, handle nulls
                }
            )
            .OrderBy(m => m.Month).ToList();

            return monthlyStatistics;
        }

        public CustomerReport CalculateNumberOfCustomersByLevel()
        {
            CustomerReport customerReport = new CustomerReport();
            var users = _context.Users.AsQueryable();
            var orders = _context.Orders;
            customerReport.Total = orders.Count();
            foreach (var user in users)
            {

                var totalAmountPurchased = orders.Where(order => order.UserId == user.Id)
            .Sum(order => (order.TotalPrice- order.DiscountAmount));
                Console.WriteLine(totalAmountPurchased); 
                switch (totalAmountPurchased)
                {
                    case var amount when amount <= 0:
                        customerReport.Level0++;
                        break;
                    case var amount when amount < 1000000:
                        customerReport.Level1k++;
                        break;
                  
                    case var amount when amount >= 1000000 && amount < 5000000:
                        customerReport.Level5k++;
                        break;
                    case var amount when amount >= 5000000 && amount < 10000000:
                        customerReport.Level10k++;
                        break;
                    case var amount when amount >= 10000000:
                        customerReport.LevelOver10k++;
                        break;
                    default:
                        // Xử lý trường hợp khác nếu cần
                        break;
                }

            }

            return customerReport;




        }

        public OrderReport CalculateOrderStatistics()
        {
            OrderReport orderReport = new OrderReport();
            var orders = _context.Orders.AsQueryable();


            // Duyệt qua danh sách đơn hàng
            foreach (var order in orders)
            {
                // Tăng biến đếm số lượng đơn hàng
                orderReport.Quantity++;

                // Tăng tổng doanh thu
                orderReport.Revenue += order.TotalPrice;

                // Kiểm tra xem trạng thái đã có trong từ điển chưa
                if (orderReport.StatusCounts.ContainsKey(order.Status))
                {
                    // Nếu đã có, tăng giá trị tương ứng
                    orderReport.StatusCounts[order.Status]++;
                }
                else
                {
                    // Nếu chưa có, thêm mới với giá trị là 1
                    orderReport.StatusCounts.Add(order.Status, 1);
                }
            }

            // Tạo và trả về đối tượng kết quả
            return orderReport;
        }

        public Dictionary<string, int> CountProductsByCategory()
        {
            Dictionary<string, int> countByCategory = new Dictionary<string, int>();
            var products = _context.Products.Include(p => p.Category).ToList();

            // Duyệt qua danh sách sản phẩm
            foreach (var product in products)
            {
                // Kiểm tra xem phân loại đã có trong từ điển chưa

                if (countByCategory.ContainsKey(product.Category.Name))
                {
                    // Nếu đã có, tăng giá trị tương ứng
                    countByCategory[product.Category.Name]++;
                }
                else
                {
                    // Nếu chưa có, thêm mới với giá trị là 1
                    countByCategory.Add(product.Category.Name, 1);
                }
            }

            // Trả về dictionary chứa kết quả
            return countByCategory;
        }

        public ProductListViewModel SortProductsByAverageReview()
        {
            var model = new ProductListViewModel();
            model.Products = _context.Products.AsQueryable().Select(p =>
                    new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,

                        AverageStar = p.Reviews.Any() ? p.Reviews.Average(r => r.Star) : 0
                    }).ToList();
            model.Products = model.Products.OrderByDescending(product => product.AverageStar).ToList();
            return model;

        }

        public ProductListViewModel SortProductsByQuantitySold()
        {
            var model = new ProductListViewModel();
            model.Products = _context.Products.AsQueryable().Select(p =>
                    new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        QuantitySold = p.QuantitySold,
                        AverageStar = p.Reviews.Any() ? p.Reviews.Average(r => r.Star) : 0
                    }).ToList();
            model.Products = model.Products.OrderByDescending(product => product.QuantitySold).ToList();
            return model;
        }
    }




}

