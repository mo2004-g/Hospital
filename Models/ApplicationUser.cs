using Microsoft.AspNetCore.Identity;

namespace Hos.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LockedAt { get; set; }


    }
}
