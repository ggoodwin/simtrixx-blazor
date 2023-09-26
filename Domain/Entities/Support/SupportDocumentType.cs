using Domain.Contracts;

namespace Domain.Entities.Support
{
    public class SupportDocumentType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
