using Domain.Contracts;
using Domain.Entities.Identity;

namespace Domain.Entities.Support
{
    public class SupportReply : AuditableEntity<int>
    {
        public string Message { get; set; }
        public int SupportTicketId { get; set; }
        public virtual SupportTicket SupportTicket { get; set; }
        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
