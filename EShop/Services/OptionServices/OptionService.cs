using EShop.Data;
using EShop.DTOs.OptionDTOs;
using EShop.Models.Products;

namespace EShop.Services.OptionServices
{
    public class OptionService : IOptionService
    {
        private readonly EShopDBContext _context;
        public OptionService(EShopDBContext context)
        {
            this._context = context;
        }

        public Option Create(OptionViewModel formData)
        {
            var option = new Option()
            {
                ProductId = formData.ProductId??0,
                Name = formData.Name,
                Price = formData.Price,
                Quantity = formData.Quantity, 
            };


            this._context.Options.Add(option);
            this._context.SaveChanges();
            return option;
        }

        public void Delete(int? id)
            
        {
            Console.WriteLine("Delete");
            var option = this._context.Options.Where(p => p.Id == id).FirstOrDefault();

            if (option == null)
            {
                throw new Exception();
            }

            this._context.Options.Remove(option);
            this._context.SaveChanges();
        }

        

        public OptionViewModel GetOptionById(int id)
        {
            var option = _context.Options.FirstOrDefault(c => c.Id == id);

            if (option == null)
            {
                throw new Exception();
            }

            var model = new OptionViewModel();

            model.Id = option.Id;
            model.Name = option.Name;
            model.Price = option.Price;
            model.ProductId = option.ProductId;
            model.Quantity = option.Quantity; 
            return model;
        }

        public async Task<Option> Update(OptionViewModel formData)
        {
            var option = this._context.Options.FirstOrDefault(o => o.Id == formData.Id);
           

            if (option == null)
            {
                throw new Exception();
            }

            option.Name = formData.Name;
            option.Price = formData.Price;
            option.Quantity = formData.Quantity; 
            this._context.Update(option);
            return option;
        }


    }
}
