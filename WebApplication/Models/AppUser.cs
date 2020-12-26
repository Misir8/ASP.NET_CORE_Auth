using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Models
{
    public class AppUser: IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DisplayName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}