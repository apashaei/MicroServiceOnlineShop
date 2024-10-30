using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductServices.Models.Entities;

namespace ProductServices.Models.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.SellNumber).HasDefaultValue(0);
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p=>p.Name).HasMaxLength(100);
        }
    }
}
