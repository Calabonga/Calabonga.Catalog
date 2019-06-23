using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Extensions;
using Calabonga.Catalog.Web.Infrastructure.Mappers.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Review
    /// </summary>
    public class ReviewMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public ReviewMapperConfiguration()
        {
            CreateMap<Review, ReviewViewModel>();

            CreateMap<ReviewCreateViewModel, Review>()
                .ForMember(x => x.Id, o=>o.Ignore())
                .ForMember(x => x.Product, o=>o.Ignore())
                .IgnoreAudit();

            CreateMap<ReviewUpdateViewModel, Review>()
                .ForMember(x => x.Product, o=>o.Ignore())
                .IgnoreAudit();

            CreateMap<Review, ReviewUpdateViewModel>()
                .ForAllMembers(x => x.Ignore());

            CreateMap<IPagedList<Review>, IPagedList<ReviewViewModel>>()
                .ConvertUsing<PagedListConverter<Review, ReviewViewModel>>();
        }
    }
}