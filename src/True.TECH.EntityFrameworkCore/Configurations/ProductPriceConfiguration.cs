using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.Products.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class ProductPriceConfiguration
        : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "ProductPrices."+ TECHConsts.DbSchema);

            builder.Property(x => x.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,4)");

            // Composite unique: one price per type per product
            builder.HasIndex(x => new { x.ProductId, x.PriceTypeId })
                   .IsUnique();

            builder.HasOne(x => x.PriceType)
                   .WithMany()
                   .HasForeignKey(x => x.PriceTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}