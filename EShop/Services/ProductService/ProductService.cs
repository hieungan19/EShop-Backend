using EShop.Data;
using EShop.DTOs;
using EShop.DTOs.Image;
using EShop.DTOs.OptionDTOs;
using EShop.DTOs.ProductDTOs;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using EShop.Services.OptionServices;

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
        public ProductViewModel GetProductById(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).Include(p => p.Images).Include(p => p.Options).Include(p => p.Category)
              .FirstOrDefault();

            if (product == null)
            {
                return null;
            }

            var model = new ProductViewModel();
            product.Category.Products = null;
            model.Id = id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Category = product.Category;
            model.CategoryId = product.CategoryId;
            model.MinPrice = product.MinPrice;
            model.MaxPrice = product.MaxPrice;
            model.Images = product.Images.Select(i => new ImageViewModel()
            {
                Id = i.Id,
                Url = i.Url,
                ProductId = i.ProductId,

            }).ToList();
            model.Options = product.Options.Select(o => new OptionViewModel()
            {
                Id = o.Id,
                Name = o.Name,
                Price = o.Price,
                ProductId = o.ProductId,
                Quantity  = o.Quantity

            }).ToList();

            return model;
        }

        public Product Create(ProductViewModel formData)
        {
            var product = new Product()
            {
                Name = formData.Name,
                Description = formData.Description,
                CategoryId = formData.CategoryId,
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

            product.MaxPrice = GetMinMaxPrice(product.Options)["Max"];
            product.MinPrice = GetMinMaxPrice(product.Options)["Min"];

            this._context.Entry(product).State = EntityState.Modified;
            this._context.SaveChanges();

            foreach (var img in formData.Images)
            {
                var image = new Image
                {
                    ProductId = productId,
                    Url = img.Url,
                };
                this._context.Images.Add(image);
            }
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

        public async Task<ProductViewModel> Update(ProductViewModel formData)
        {
            var product = this._context.Products.Where(p => p.Id == formData.Id).FirstOrDefault();

            if (product == null)
            {
                throw new Exception();
            }
            if (formData.Name != "")
            {
                product.Name = formData.Name;
            }
            if (formData.Description != "")
            {
                product.Description = formData.Description;
            }

            if (formData.CategoryId != 0 && formData.CategoryId > 0)
            {
                product.CategoryId = formData.CategoryId;
            }


            var options = _context.Options.Where(o => o.ProductId == formData.Id).ToList().Select(o=> new OptionViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Price = o.Price,
                ProductId = o.ProductId, 
                Quantity = o.Quantity, 
            });
           

            foreach(var opt in formData.Options)
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

        public ProductListViewModel GetPaginatedProducts(FilterViewModel filters)
        {
            int page = filters.CurrentPage != 0 ? filters.CurrentPage : 1;
            var query = _context.Products.Include(p => p.Images).AsQueryable();
            if (filters.PerPage == 0) filters.PerPage = query.Count();
            query = FilterQuery(query, filters);

            var products = query.Skip((page - 1) * filters.PerPage).Take(filters.PerPage)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    MaxPrice = p.MaxPrice,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Images = p.Images.Select(i => new ImageViewModel()
                    {
                        Id = i.Id,
                        Url = i.Url,
                        ProductId = i.ProductId,

                    }).ToList(),
                }).ToList();

            var model = new ProductListViewModel();
            model.Products = products;
            model.TotalPages = this.GetTotalPages(filters.CategoryId, filters.PriceFrom, filters.PriceTo, filters.PerPage);

            return model;
        }

      
    }
}

