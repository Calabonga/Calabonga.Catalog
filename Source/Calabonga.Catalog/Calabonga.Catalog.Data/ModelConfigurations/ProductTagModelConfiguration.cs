using Calabonga.Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Catalog.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="ProductTag"/>
    /// </summary>
    public class ProductTagModelConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.TagId });

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductTags)
                .HasForeignKey(x => x.ProductId);

            builder
                .HasOne(x => x.Tag)
                .WithMany(x => x.ProductTags)
                .HasForeignKey(x => x.TagId);
        }
    }
}
