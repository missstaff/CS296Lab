using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        public AppUser Commenter { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime Date { get; set; }

    }
}
