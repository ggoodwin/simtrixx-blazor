using Domain.Contracts;
using Domain.Entities.ExtendedAttributes;

namespace Domain.Entities.Support
{
    public class SupportDocument : AuditableEntityWithExtendedAttributes<int, int, SupportDocument, SupportDocumentExtendedAttribute>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int SupportDocumentTypeId { get; set; }
        public virtual SupportDocumentType SupportDocumentType { get; set; }
    }
}
