using Application.Features.DemoRequests.Commands.AddEdit;
using Application.Features.DemoRequests.Queries.GetAll;
using Application.Features.DemoRequests.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DemoRequestProfile : Profile
    {
        public DemoRequestProfile()
        {
            CreateMap<AddEditDemoRequestCommand, DemoRequest>().ReverseMap();
            CreateMap<GetDemoRequestByIdResponse, DemoRequest>().ReverseMap();
            CreateMap<GetAllDemoRequestsResponse, DemoRequest>().ReverseMap();
        }
    }
}
