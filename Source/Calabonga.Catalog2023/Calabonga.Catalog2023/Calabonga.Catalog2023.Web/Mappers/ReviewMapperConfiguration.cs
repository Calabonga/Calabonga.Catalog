using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

namespace Calabonga.Catalog2023.Web.Mappers;

public class ReviewMapperConfiguration : Profile
{
    public ReviewMapperConfiguration()
    {
        CreateMap<Review, ReviewViewModel>();
    }
}