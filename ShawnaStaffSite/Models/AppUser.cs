using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shawna_Staff.Models
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public IList<string> RolesNames { get; set; } 

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }
}
