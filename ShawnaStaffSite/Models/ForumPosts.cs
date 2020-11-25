using System;
using System.ComponentModel.DataAnnotations;

namespace Shawna_Staff.Models
{
    public class ForumPosts
    {
        [Key]
        public int PostID { get; set; }


        [StringLength(60, MinimumLength = 2, ErrorMessage = "Post title must be between 2 and 60 characters")]
        [Required]
        public string PostTopic { get; set; }


        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Post must be between 2 and 1000 characters")]
        [Required]
        public string PostText { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public User Name { get; set; }

        [Range(1, 5, ErrorMessage = "Ratings must be between 1 and 5.")]
        [Required(ErrorMessage = "Please enter a rating.")]
        public int PostRating { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
