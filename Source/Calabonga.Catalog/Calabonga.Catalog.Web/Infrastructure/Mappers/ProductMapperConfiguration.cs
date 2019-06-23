using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Extensions;
using Calabonga.Catalog.Web.Infrastructure.Mappers.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Product
    /// </summary>
    public class ProductMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public ProductMapperConfiguration()
        {
            CreateMap<Product, ProductViewModel>();

            CreateMap<ProductCreateViewModel, Product>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Reviews, o => o.Ignore())
                .ForMember(x => x.Category, o => o.Ignore())
                .IgnoreAudit();

            CreateMap<ProductUpdateViewModel, Product>()
                .ForMember(x => x.Reviews, o => o.Ignore())
                .ForMember(x => x.Category, o => o.Ignore())
                .IgnoreAudit();

            CreateMap<Product, ProductUpdateViewModel>();

            CreateMap<IPagedList<Product>, IPagedList<ProductViewModel>>()
                .ConvertUsing<PagedListConverter<Product, ProductViewModel>>();
        }
    }
}