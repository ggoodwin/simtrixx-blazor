using Domain.Contracts;

namespace Domain.Entities
{
    public class ContactRequest : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Contacted { get; set; }
        public string? Notes { get; set; }
    }
}
