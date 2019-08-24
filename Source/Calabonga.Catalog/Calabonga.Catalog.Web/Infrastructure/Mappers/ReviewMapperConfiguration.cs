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
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Visible, o => o.MapFrom(_ => false))
                .ForMember(x => x.Product, o => o.Ignore())
                .IgnoreAudit();

            CreateMap<ReviewUpdateViewModel, Review>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Content, o => o.MapFrom(p => p.Content))
                .ForMember(x => x.Rating, o => o.MapFrom(p => p.Rating))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<Review, ReviewUpdateViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Content, o => o.MapFrom(p => p.Content))
                .ForMember(x => x.Rating, o => o.MapFrom(p => p.Rating))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<IPagedList<Review>, IPagedList<ReviewViewModel>>()
                .ConvertUsing<PagedListConverter<Review, ReviewViewModel>>();
        }
    }
}