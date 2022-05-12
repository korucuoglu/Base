using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Api.Domain.Entities;

namespace Base.Api.Persistence.EntityConfigurations
{
    public class ProductEntityConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("products");

            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            builder.Property(x => x.ShortName).HasColumnName("short_name").IsRequired();
            builder.Property(x => x.Image).HasColumnName("image");
            builder.Property(x => x.Url).HasColumnName("url").IsRequired();
        }
    }
}