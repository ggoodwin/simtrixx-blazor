using Application.Responses.Identity;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, SimtrixxRole>().ReverseMap();
        }
    }
}
