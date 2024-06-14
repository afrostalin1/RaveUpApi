using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ReviewComment
{
    /// <summary>
    /// A Data Transfer Object representing a ReviewComment.
    /// </summary>
    public class ReviewCommentDto
    {
        public int Id { get; set; }

        public int? ReviewId { get; set; } // Navigation property

        public string CommentBody { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}