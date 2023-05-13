using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;

namespace Calabonga.Catalog2023.Web.Mappers;

public class ReviewMapperConfiguration : Profile
{
    public ReviewMapperConfiguration()
    {
        CreateMap<Review, ReviewViewModel>();
        CreateMap<ReviewCreateViewModel, Review>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedBy, o => o.Ignore())
            .ForMember(x => x.CreatedBy, o => o.MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(x => x.Visible, o => o.MapFrom(_ => false));
    }
}