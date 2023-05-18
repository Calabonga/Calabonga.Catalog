using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

namespace Calabonga.Catalog2023.Web.Mappers;

public class ProductMapperConfiguration : Profile
{
    public ProductMapperConfiguration()
    {
        CreateMap<ProductPostViewModel, Product>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Category, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForMember(x => x.Visible, o => o.MapFrom(p => false))
            .ForMember(x => x.Tags, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedBy, o => o.Ignore())
            .ForMember(x => x.CreatedBy, o => o.MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]));

        CreateMap<Product, ProductViewModel>();

        CreateMap<Review, ReviewForProductViewModel>();

        CreateMap<ProductUpdateViewModel, Product>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Category, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForMember(x => x.Tags, o => o.Ignore())
            .ForMember(x => x.Visible, o => o.MapFrom(p => p.Visible))
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CreatedBy, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedBy, o => o.MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]));
    }
}