using Microsoft.AspNetCore.Identity;

namespace solution.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
