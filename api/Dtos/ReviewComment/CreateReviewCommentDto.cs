using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
// You will put your data validation in the dto
namespace api.Dtos.ReviewComment
{
    /// <summary>
    /// Data Transfer Object representing the creation of a ReviewComment in a HTTPPost method
    /// </summary>
    public class CreateReviewCommentDto
    {
        //Here you set that the Body is required, the min length is 5 and the max length is 500
        [Required]
        [MinLength(5, ErrorMessage = "Comment must be at least 5 characters")]
        [MaxLength(500, ErrorMessage = "Comment can not be over 500 characters")]
        public string CommentBody { get; set; } = string.Empty;
    }
}
