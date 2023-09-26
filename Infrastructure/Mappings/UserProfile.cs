using Application.Responses.Identity;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, SimtrixxUser>().ReverseMap();
        }
    }
}
