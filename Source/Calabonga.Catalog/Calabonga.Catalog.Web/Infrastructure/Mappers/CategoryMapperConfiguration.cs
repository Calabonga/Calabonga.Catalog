using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Mappers.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 12:44 CategoryMapperConfiguration)
    /// </summary>
    public class CategoryMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public CategoryMapperConfiguration()
        {
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Category, CategoryUpdateViewModel>();

            CreateMap<CategoryUpdateViewModel, Category>()
                .ForMember(x => x.Products, o => o.Ignore());
            
            CreateMap<CategoryCreateViewModel, Category>()
                .ForMember(x=>x.Id, o=>o.Ignore())
                .ForMember(x=>x.Products, o=>o.Ignore());

            CreateMap<IPagedList<Category>, IPagedList<CategoryViewModel>>()
                .ConvertUsing<PagedListConverter<Category, CategoryViewModel>>();
        }
    }
}
