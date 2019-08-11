using System.Linq;
using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers.Resolvers
{
    /// <summary>
    /// Resolver for Tag
    /// </summary>
    public class TagToStringResolver : IValueResolver<Product, ProductUpdateViewModel, string>
    {
        private readonly ITagService _tagService;

        public TagToStringResolver(ITagService tagService)
        {
            _tagService = tagService;
        }

        /// <inheritdoc />
        public string Resolve(Product source, ProductUpdateViewModel destination, string destMember, ResolutionContext context)
        {
            var tags = _tagService.GetStringFromTags(source.Id);
            return string.Join(";", tags);
        }
    }
}