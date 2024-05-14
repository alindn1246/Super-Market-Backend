using Microsoft.AspNetCore.Identity;

namespace HussieniSuperMarket.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

    }
}
