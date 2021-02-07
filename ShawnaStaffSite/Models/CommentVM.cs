using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class CommentVM
    {
        public int PostID { get; set; }
        public string PostTopic { get; set; }

        public string CommentText { get; set; }
    }
}
