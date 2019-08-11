using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers.Resolvers
{
    /// <summary>
    /// Resolver for Tags
    /// </summary>
    public class StringToTagResolver : IValueResolver<ITagsHolder, Product, ICollection<ProductTag>>
    {
        private readonly ITagService _tagService;

        /// <inheritdoc />
        public StringToTagResolver(ITagService tagService)
        {
            _tagService = tagService;
        }

        /// <inheritdoc />
        public ICollection<ProductTag> Resolve(ITagsHolder source, Product destination, ICollection<ProductTag> destMember, ResolutionContext context)
        {
            var tags = _tagService.GetTagsFromString(destination.Id, source.TagsAsString);
            return new Collection<ProductTag>(tags.ToList());
        }
    }
}