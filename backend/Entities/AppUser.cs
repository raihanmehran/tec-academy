using Microsoft.AspNetCore.Identity;

namespace backend.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Country { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}