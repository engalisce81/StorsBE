using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using True.TECH.Entities.PriceTypes.Entites;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace True.TECH.Configurations
{
    public class PriceTypeConfiguration
        : IEntityTypeConfiguration<PriceType>
    {
        public void Configure(EntityTypeBuilder<PriceType> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable(TECHConsts.DbTablePrefix + "PriceTypes." +TECHConsts.DbSchema);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.Description)
                   .HasMaxLength(512);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
