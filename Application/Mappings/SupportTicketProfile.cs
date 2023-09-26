using Application.Features.SupportReplys.Commands.AddEdit;
using Application.Features.SupportTickets.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAll;
using Application.Features.SupportTickets.Queries.GetAllByStatus;
using Application.Features.SupportTickets.Queries.GetAllByUserId;
using Application.Features.SupportTickets.Queries.GetAllReplies;
using AutoMapper;
using Domain.Entities.Support;

namespace Application.Mappings
{
    public class SupportTicketProfile : Profile
    {
        public SupportTicketProfile()
        {
            CreateMap<GetAllSupportTicketsByUserIdResponse, SupportTicket>().ReverseMap();
            CreateMap<GetAllSupportTicketsByStatusResponse, SupportTicket>().ReverseMap();
            CreateMap<GetAllSupportTicketsResponse, SupportTicket>().ReverseMap();
            CreateMap<GetAllSupportRepliesResponse, SupportReply>().ReverseMap();

            CreateMap<AddEditSupportTicketCommand, SupportTicket>().ReverseMap();
            CreateMap<AddEditSupportReplyCommand, SupportReply>().ReverseMap();
        }
    }
}
