using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.Products.Entites;
using True.TECH.Entities.Stores.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class StoreConfiguration
        : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "Stores."+  TECHConsts.DbSchema);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.Location)
                   .HasMaxLength(512);

            builder.HasIndex(x => x.Name);

            builder.HasMany(x=>x.Products)
                   .WithOne(x => x.Store)
                   .HasForeignKey(x => x.StoreId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}