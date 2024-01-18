using EShop.Data;
using EShop.DTOs;
using EShop.DTOs.Image;
using EShop.DTOs.OptionDTOs;
using EShop.DTOs.ProductDTOs;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using EShop.Services.OptionServices;
using EShop.DTOs.ReviewDTOs;
using EShop.Models.CouponModel;

namespace EShop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly EShopDBContext _context;
        private readonly IOptionService _optionService;

       
        public ProductService(EShopDBContext context,IOptionService optionService )
        {
            this._context = context;
            _optionService = optionService;
        }

        public Dictionary<string, double> GetMinMaxPrice(ICollection<Option> options)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            result["Min"] = options.ElementAt(0).Price;
            result["Max"] = options.ElementAt(0).Price;
            foreach (Option opt in options)
            {
                result["Min"] = Math.Min(result["Min"], opt.Price);
                result["Max"] = Math.Max(result["Max"], opt.Price);
            }
            return result;
        }

        public ProductViewModel GetCurrentMinMaxPrice(ProductViewModel product, Coupon? coupon)
        {

            if (coupon != null)
            {
                if (coupon.DiscountAmount != null)
                {
                    product.CurrentMaxPrice = product.MaxPrice - coupon.DiscountAmount;
                    product.CurrentMinPrice = product.MinPrice - coupon.DiscountAmount;
                }
                else
                {
                    product.CurrentMaxPrice = product.MaxPrice * (1 -  coupon.DiscountPercent/ 100);
                    product.CurrentMinPrice = product.MinPrice * (1 - coupon.DiscountPercent / 100);
                }


            }
            else
            {
                product.CurrentMaxPrice = product.MaxPrice;
                product.CurrentMinPrice = product.MinPrice; 
            }
            return product;
        }
        public ProductViewModel GetProductById(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).Include(p => p.Options).Include(p => p.Category).Include(p=>p.Reviews).Include(p=>p.CurrentCoupon).FirstOrDefault();

            if (product == null)
            {
                return null;
            }

            var model = new ProductViewModel();
            product.Category.Products = null;
            model.Id = id;
            model.AverageStar = product.Reviews.Any() ? product.Reviews.Average(r => r.Star) : 0; 

            model.Name = product.Name;
            model.Description = product.Description;
            model.Category = product.Category;
            model.CategoryId = product.CategoryId;
            model.MinPrice = product.MinPrice;
            model.MaxPrice = product.MaxPrice;
            model.CurrentCouponId = product.CurrentCouponId;
            model.ImageUrl = product.ImageUrl;
            model.CurrentCoupon = product.CurrentCoupon;
            model.QuantitySold = product.QuantitySold;
            model = GetCurrentMinMaxPrice(model,model.CurrentCoupon);
            // current price
            if (model.CurrentCoupon!=null)
            model.CurrentCoupon.Products = null; 
            model.Options = product.Options.Select(o => new OptionViewModel()
            {
                Id = o.Id,
                Name = o.Name,
                Price = o.Price,
                ProductId = o.ProductId,
                Quantity  = o.Quantity

            }).ToList();



            model.Reviews = product.Reviews.Select(r => {
               
                var user = _context.Users.Where(u => u.Id==r.UserId).FirstOrDefault(); 
                return
                new ReviewViewModel()
                {
                    Detail = r.Detail ?? "",
                    Star = r.Star,
                    UserId = r.UserId,
                    UserName = user?.FullName,
                    Avatar = user?.AvatarUrl,
                    CreatedDate = r.CreatedDate, 

                };

            }).ToList();

            return model;
        }

        public Product Create(ProductViewModel formData)
        {
            var product = new Product()
            {
                Name = formData.Name,
                Description = formData.Description,
                CategoryId = formData.CategoryId??0,
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            int productId = product.Id;
            Console.WriteLine(productId);
            foreach (var opt in formData.Options)
            {
                var option = new Option
                {
                    ProductId = productId,
                    Price = opt.Price,
                    Name = opt.Name,
                    Quantity = opt.Quantity,

                };
                this._context.Options.Add(option);
                
            }
            product.CurrentCouponId = formData.CurrentCouponId; 
            product.MaxPrice = GetMinMaxPrice(product.Options)["Max"];
            product.MinPrice = GetMinMaxPrice(product.Options)["Min"];
            Console.WriteLine(product.MinPrice); 
            product.ImageUrl = formData.ImageUrl;

            this._context.Entry(product).State = EntityState.Modified;
            this._context.SaveChanges();

           
            _context.SaveChanges();

            return product;
        }

        public void Delete(int id)
        {
            var product = this._context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new Exception();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public async Task<ProductViewModel> Update(int id, ProductViewModel formData)
        {
            var product = this._context.Products.Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
            {
                throw new Exception();
            }
            if (formData.Name != null)
            {
                product.Name = formData.Name;
            }
            if (formData.Description != null)
            {
                product.Description = formData.Description;
            }

            if (formData.CategoryId != 0 && formData.CategoryId > 0)
            {
                product.CategoryId = formData.CategoryId ?? 0; 
            }
            if (formData.ImageUrl != null )
            {
                Console.WriteLine(formData.ImageUrl); 
                product.ImageUrl = formData.ImageUrl;
            }

            if (formData.Options != null)
            {

                var options = _context.Options.Where(o => o.ProductId == id).ToList().Select(o => new OptionViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Price,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                });

                // Tạo option mới
                foreach (var opt in formData.Options)
                {
                    if (opt.Id == null)
                    {
                        _optionService.Create(opt);
                    }
                }

                foreach (var opt in options)
                {
                    //change options
                    var i = formData.Options.FindIndex(option => option.Id == opt.Id);
                    Console.WriteLine(i);
                    if (i >= 0)
                    {
                        _optionService.Update(formData.Options[i]);
                    }
                    else
                    {
                        _optionService.Delete(opt.Id);
                    }

                }

                product.MaxPrice = GetMinMaxPrice(product.Options)["Max"];
                product.MinPrice = GetMinMaxPrice(product.Options)["Min"];
            }
            
            if (formData.CurrentCouponId != 0)
            {
                product.CurrentCouponId = formData.CurrentCouponId;
            }

            product.CurrentCoupon = _context.Coupons.Where(c => c.Id == formData.CurrentCouponId).FirstOrDefault(); 
            Console.WriteLine(product.CurrentCoupon);
            this._context.Entry(product).State = EntityState.Modified;
            this._context.SaveChanges();

            return GetProductById(product.Id);
        }

        public double GetTotalPages(int categoryId = 0, double priceFrom = 0, double priceTo = 0, int perPage = 0)
        {
            IQueryable<Product> query;

            if (categoryId == 0)
                query = _context.Products.AsQueryable();
            else
                query = _context.Products.Where(p => p.CategoryId == categoryId);

            if (priceFrom != 0)
                query = query.Where(p => p.MinPrice >= priceFrom);
            if (priceTo != 0)
                query = query.Where(p => p.MinPrice <= priceTo);

            double totalProducts = query.Count();
            double pages = totalProducts / perPage;
            pages = Math.Ceiling(pages);
            return pages;
        }
        public IQueryable<Product> FilterQuery(IQueryable<Product> query, FilterViewModel filters)
        {
            if (filters.CategoryId != 0)
            {
                query = query.Where(p => p.CategoryId == filters.CategoryId);
            }
            if (filters.PriceFrom != 0)
            {
                query = query.Where(p => p.MinPrice >= filters.PriceFrom);
            }
            if (filters.PriceTo != 0)
            {
                query = query.Where(p => p.MaxPrice <= filters.PriceTo);
            }

            //1 - newest first
            //2 - lowest price first
            //3 - hightest price first
            if (filters.Order == 2)
            {
                query = query.OrderBy(p => p.MinPrice);
            }
            if (filters.Order == 3)
            {
                query = query.OrderByDescending(p => p.MinPrice);
            }

            return query;
        }

        public async Task<ProductListViewModel> GetPaginatedProducts(FilterViewModel filters)
        {
            int page = filters.CurrentPage != 0 ? filters.CurrentPage : 1;
            var query = _context.Products.Include(p => p.CurrentCoupon).Include(p => p.Category).Include(p => p.Reviews).AsQueryable();
            DateTime currentDate = DateTime.Now;

            foreach (var product in query)
            {
                var currentCoupon = product.CurrentCoupon;

                if (currentCoupon != null && currentDate > currentCoupon.EndDate)
                {
                    // Coupon đã hết hạn, cập nhật CurrentCouponId = null
                    product.CurrentCouponId = null;
                }
            }

            await _context.SaveChangesAsync(); // Await the SaveChangesAsync method

            if (filters.PerPage == 0) filters.PerPage = query.Count();
            query = FilterQuery(query, filters);

            var products = query.Skip((page - 1) * filters.PerPage).Take(filters.PerPage)
                .Select(p =>
                    new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        MaxPrice = p.MaxPrice,
                        MinPrice = p.MinPrice,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CurrentCouponId = p.CurrentCouponId,
                        CurrentCoupon = p.CurrentCoupon,
                        ImageUrl = p.ImageUrl,
                        Category = p.Category,
                        QuantitySold = p.QuantitySold,
                        AverageStar = p.Reviews.Any() ? p.Reviews.Average(r => r.Star) : 0
                    }).ToList();

            var model = new ProductListViewModel();
            model.Products = products;
            model.TotalPages = this.GetTotalPages(filters.CategoryId, filters.PriceFrom, filters.PriceTo, filters.PerPage);

            return model;
        }


        public async Task UpdatCurrentDiscount(int productId, int? newCurrentCoupon)
        {
            var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault(); 
            if (product == null) return;
            product.CurrentCouponId = newCurrentCoupon; 
            
                
        }


    }
}

