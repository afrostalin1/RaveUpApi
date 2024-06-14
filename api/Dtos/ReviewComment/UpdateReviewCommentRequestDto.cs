using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ReviewComment
{
    /// <summary>
    /// Data Transfer Object representing the creation of a ReviewComment in a HTTPPut method
    /// </summary>
    public class UpdateReviewCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Comment must be at least 5 characters")]
        [MaxLength(500, ErrorMessage = "Comment can not be over 500 characters")]
        public string CommentBody { get; set; } = string.Empty;
    }
}