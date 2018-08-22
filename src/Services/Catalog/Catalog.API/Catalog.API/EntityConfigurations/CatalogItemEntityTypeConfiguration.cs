using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.EntityConfigurations
{
    class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cp => cp.Price)
                .IsRequired();

            builder.Property(cpf => cpf.PictureFileName)
                .IsRequired(false);

            builder.Ignore(cpu => cpu.PictureUri);

            builder.HasOne(cb => cb.CatalogBrand)
                .WithMany()
                .HasForeignKey(cb => cb.CatalogBrandId);

            builder.HasOne(ct => ct.CatalogType)
                .WithMany()
                .HasForeignKey(ct => ct.CatalogTypeId);
        }
    }
}
