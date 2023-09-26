using Domain.Enums;

namespace Application.Features.SupportTickets.Queries.GetReplyById
{
    public class GetSupportReplyByIdResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SupportTicketId { get; set; }
        public string SimtrixxUserId { get; set; }
        public string UserName { get; set; }
    }
}
