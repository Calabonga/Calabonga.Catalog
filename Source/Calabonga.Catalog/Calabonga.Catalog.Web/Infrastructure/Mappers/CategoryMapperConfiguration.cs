using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Controllers;
using Calabonga.Catalog.Web.Infrastructure.Mappers.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 12:44 CategoryMapperConfiguration)
    /// </summary>
    public class CategoryMapperConfiguration : MapperConfigurationBase
    {
        public CategoryMapperConfiguration()
        {
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Category, CategoryUpdateViewModel>();

            CreateMap<IPagedList<Category>, IPagedList<CategoryViewModel>>()
                .ConvertUsing<PagedListConverter<Category, CategoryViewModel>>();
        }
    }
}
