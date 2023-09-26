using Domain.Enums;

namespace Application.Features.SupportTickets.Queries.GetAllByUserId
{
    public class GetAllSupportTicketsByUserIdResponse
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public SupportPriority Priority { get; set; }
        public SupportStatus Status { get; set; }
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public string SimtrixxUserId { get; set; }
    }
}
