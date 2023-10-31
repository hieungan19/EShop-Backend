using EShop.DTOs.OptionDTOs;
using EShop.Models.Products;

namespace EShop.Services.OptionServices
{
    public interface IOptionService
    {
        public OptionViewModel GetOptionById(int id);

        public Task<Option> Update(OptionViewModel formData);
        public Option Create(OptionViewModel formData);
        public void Delete(int? id);
    }
}
