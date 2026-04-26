using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.Products.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class ProductStockConfiguration
        : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "ProductStocks."  +TECHConsts.DbSchema);

            builder.Property(x => x.Quantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.HasIndex(x => new { x.ProductId, x.StoreId })
                   .IsUnique();

            builder.HasOne(x => x.Store)
                   .WithMany(x=>x.Products)
                   .HasForeignKey(x => x.StoreId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.Stocks)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}