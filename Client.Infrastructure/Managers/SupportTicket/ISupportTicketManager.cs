using Application.Features.SupportTickets.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAll;
using Application.Features.SupportTickets.Queries.GetById;
using Common.Wrapper;
using Domain.Enums;

namespace Client.Infrastructure.Managers.SupportTicket
{
    public interface ISupportTicketManager : IManager
    {
        Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>> GetAllSupportTicketsAsync();
        Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>> GetAllSupportTicketsByUserAsync(string userId);

        Task<IResult<IEnumerable<GetAllSupportTicketsResponse>>>
            GetAllSupportTicketsByStatusAsync(SupportStatus status);

        Task<IResult<GetSupportReplyByIdResponse>> GetSupportTicketByIdAsync(GetSupportReplyByIdQuery request);
        Task<IResult<int>> SaveAsync(AddEditSupportTicketCommand request);
        Task<IResult<int>> DeleteAsync(int id);
    }
}
