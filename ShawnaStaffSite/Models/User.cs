using System.ComponentModel.DataAnnotations;

namespace Shawna_Staff.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }

    }
}
