using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class UserVM
    {
        public IEnumerable<AppUser> Users { get; set; } 
        public IEnumerable<AppUser> Roles { get; set; }
    }
}
