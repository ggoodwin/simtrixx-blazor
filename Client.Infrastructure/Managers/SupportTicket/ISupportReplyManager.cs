using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.SupportReplys.Commands.AddEdit;
using Application.Features.SupportTickets.Queries.GetAllReplies;
using Application.Features.SupportTickets.Queries.GetReplyById;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.SupportTicket
{
    public interface ISupportReplyManager
    {
        Task<IResult<IEnumerable<GetAllSupportRepliesResponse>>> GetAllSupportRepliesAsync(int supportTicketId);
        Task<IResult<GetSupportReplyByIdResponse>> GetSupportReplyByIdAsync(GetSupportReplyByIdQuery request);
        Task<IResult<int>> SaveAsync(AddEditSupportReplyCommand request);
        Task<IResult<int>> DeleteAsync(int id);
    }
}
