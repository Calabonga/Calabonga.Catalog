using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure.ModelConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Catalog2023.Infrastructure.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="Category"/>
    /// </summary>
    public class CategoryModelConfiguration : IdentityModelConfigurationBase<Category>
    {
        /// <inheritdoc />
        protected override void AddBuilder(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.Visible);

            builder.HasMany(x => x.Products);
        }

        /// <inheritdoc />
        protected override string TableName()
        {
            return "Categories";
        }
    }
}
