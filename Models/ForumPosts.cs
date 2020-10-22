using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class ForumPosts
    {
        public string PostTopic { get; set; }

        public string PostText { get; set; }

        public User UserName { get; set; }

        public string PostRating { get; set; }

        public DateTime Date { get; set; }
    }
}
