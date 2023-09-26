using Domain.Contracts;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class License : AuditableEntity<int>
    {
        public string Key { get; set; }
        public DateTime? Expiration { get; set; }
        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
