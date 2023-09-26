using Domain.Contracts;
using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities.Support
{
    public class SupportTicket : AuditableEntity<int>
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public SupportPriority Priority { get; set; }
        public SupportStatus Status { get; set; }

        public int SupportDepartmentId { get; set; }
        public virtual SupportDepartment SupportDepartment { get; set; }
        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
