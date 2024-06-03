using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ReviewComment
{
    public class ReviewCommentDto
    {
        public int Id { get; set; }
        public int? ReviewId { get; set; } // Navigation property
        public string Body { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;



    }
}