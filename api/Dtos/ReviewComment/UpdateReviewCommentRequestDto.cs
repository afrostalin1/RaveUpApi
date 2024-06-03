using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ReviewComment
{
    public class UpdateReviewCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Comment must be at least 5 characters")]
        [MaxLength(500, ErrorMessage = "Comment can not be over 500 characters")]
        public string Body { get; set; } = string.Empty;
    }
}