using Domain.Contracts;

namespace Domain.Entities
{
    public class DemoRequest : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Contacted { get; set; }
        public string? Notes { get; set; }
    }
}
