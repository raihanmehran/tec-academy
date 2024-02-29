using Microsoft.AspNetCore.Identity;

namespace backend.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}