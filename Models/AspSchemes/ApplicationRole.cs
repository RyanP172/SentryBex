using Microsoft.AspNetCore.Identity;

namespace SentryBex.Models.AspSchemes
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; } = null!;
        public virtual ICollection<IdentityRoleClaim<string>> AspNetRoleClaims { get; set; } = null!;
    }
}
