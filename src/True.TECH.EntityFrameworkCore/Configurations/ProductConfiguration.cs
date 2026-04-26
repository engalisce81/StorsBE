using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.Products.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class ProductConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "Products."+TECHConsts.DbSchema);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.Prices)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Stocks)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Images)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}