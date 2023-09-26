using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class SimtrixxRole : IdentityRole, IAuditableEntity<string>
    {
        public string Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<SimtrixxRoleClaim> RoleClaims { get; set; }

        public SimtrixxRole() : base()
        {
            RoleClaims = new HashSet<SimtrixxRoleClaim>();
        }

        public SimtrixxRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<SimtrixxRoleClaim>();
            Description = roleDescription;
        }
    }
}
