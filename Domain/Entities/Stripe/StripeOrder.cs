using Domain.Contracts;
using Domain.Entities.Identity;

namespace Domain.Entities.Stripe
{
    public class StripeOrder : AuditableEntity<int>
    {
        public string StripeOrderId { get; set; }
        public string Email { get; set; }

        public int LicenseId { get; set; }
        public virtual License License { get; set; }
        public int StripeSubscriptionId { get; set; }
        public virtual StripeSubscription StripeSubscription { get; set; }
        public int StripeCustomerId { get; set; }
        public virtual StripeCustomer StripeCustomer { get; set; }
        public string SimtrixxUserId { get; set; }
        public virtual SimtrixxUser SimtrixxUser { get; set; }
    }
}
