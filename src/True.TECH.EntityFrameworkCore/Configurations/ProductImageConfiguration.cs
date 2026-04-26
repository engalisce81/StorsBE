using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.Products.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "ProductImages."+ TECHConsts.DbSchema);

            builder.Property(x => x.FileName)
                   .IsRequired()
                   .HasMaxLength(256); 

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasIndex(x => x.ProductId);
        }
    }
}