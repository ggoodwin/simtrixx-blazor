using Domain.Contracts;
using Domain.Entities.Identity;

namespace Domain.Entities.Stripe
{
    public class StripeCustomer : AuditableEntity<int>
    {
        public string StripeCustomerId { get; set; }

        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
