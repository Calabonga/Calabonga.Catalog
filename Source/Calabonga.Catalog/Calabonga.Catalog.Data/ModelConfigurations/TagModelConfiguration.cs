using Calabonga.Catalog.Data.ModelConfigurations.Base;
using Calabonga.Catalog.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Catalog.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="Tag"/>
    /// </summary>
    public class TagModelConfiguration: IdentityModelConfigurationBase<Tag>
    {
        /// <inheritdoc />
        protected override void AddBuilder(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

        }

        /// <inheritdoc />
        protected override string TableName()
        {
            return "Tags";
        }
    }
}
