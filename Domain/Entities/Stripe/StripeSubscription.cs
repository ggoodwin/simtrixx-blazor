using Domain.Contracts;
using Domain.Entities.Identity;

namespace Domain.Entities.Stripe
{
    public class StripeSubscription : AuditableEntity<int>
    {
        public string StripeSubscriptionId { get; set; }

        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
