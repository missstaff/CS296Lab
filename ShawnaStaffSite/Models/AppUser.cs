using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shawna_Staff.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }
}
