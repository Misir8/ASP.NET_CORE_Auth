using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}