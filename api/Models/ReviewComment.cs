using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    
    public class ReviewComment
    {
        public int Id { get; set; }
        public int? ReviewId { get; set; } // Navigation property
        public Review? Review { get; set; }
        public string CommentBody { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}