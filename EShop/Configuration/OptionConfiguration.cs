using EShop.Models.CategoryModel;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class OptionConfiguration: IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne<Product>(o => o.Product)
               .WithMany(p => p.Options)
               .HasForeignKey(o => o.ProductId);


        }

       
    }
}
