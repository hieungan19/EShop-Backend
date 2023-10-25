
using EShop.Models.CategoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasData
                (
                    new Category
                    {
                        Id = 1,
                        Name = "Category 1",
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Category 2",
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Category 3",
                    }
                );
        }
    }
}
