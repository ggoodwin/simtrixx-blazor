using Application.Requests.Identity;
using Application.Responses.Identity;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, SimtrixxRoleClaim>()
                .ForMember(nameof(SimtrixxRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(SimtrixxRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, SimtrixxRoleClaim>()
                .ForMember(nameof(SimtrixxRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(SimtrixxRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}
