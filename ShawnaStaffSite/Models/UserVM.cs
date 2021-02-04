using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class UserVM
    {
        public IEnumerable<AppUser> Users { get; set; } 
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
