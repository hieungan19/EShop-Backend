using Microsoft.EntityFrameworkCore;
using EShop.Models.Products;
using EShop.Models.CategoryModel;

namespace EShop.Configuration.Products; 
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Category>(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);

        builder.HasData
               (
                   new Product
                   {
                       Id = 1,
                       Name = "Product 1",
                       Description = "Test Product",
                       CategoryId = 1,
                   },
                   new Product
                   {
                       Id = 2,
                       Name = "Product 2",
                       Description = "Test Product",
                       CategoryId = 1,
                   });
    }
}
