using Application.Features.ContactRequests.Commands.AddEdit;
using Application.Features.ContactRequests.Queries.GetAll;
using Application.Features.ContactRequests.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ContactRequestProfile : Profile
    {
        public ContactRequestProfile()
        {
            CreateMap<AddEditContactRequestCommand, ContactRequest>().ReverseMap();
            CreateMap<GetContactRequestByIdResponse, ContactRequest>().ReverseMap();
            CreateMap<GetAllContactRequestsResponse, ContactRequest>().ReverseMap();
        }
    }
}
