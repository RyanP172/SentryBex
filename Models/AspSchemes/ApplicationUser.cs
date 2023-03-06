using Microsoft.AspNetCore.Identity;
using System.Data;

namespace SentryBex.Models.AspSchemes
{
    public class ApplicationUser : IdentityUser
    {
        /*public ApplicationUser()
        {
            AspNetUserClaims = new HashSet<IdentityUserClaim<string>>();
            AspNetUserLogins = new HashSet<IdentityUserLogin<string>>();
            AspNetRoles = new HashSet<IdentityUserRole<string>>();
        }*/

        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; } = null!;
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = null!;
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = null!;
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; } = null!;
    }
}
