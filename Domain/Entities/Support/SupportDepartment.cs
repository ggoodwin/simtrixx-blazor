using Domain.Contracts;

namespace Domain.Entities.Support
{
    public class SupportDepartment : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
