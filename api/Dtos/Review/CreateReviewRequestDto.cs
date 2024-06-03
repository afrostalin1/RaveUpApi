using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Review
{
    public class CreateReviewRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title cannot be over 50 charaters")]
        public string Title { get; set; } = string.Empty;


        [Required]
        [MaxLength(50, ErrorMessage = "Venue cannot be over 50 charaters")]
        public string Venue { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Genre cannot be over 50 charaters")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Comment must be at least 5 characters")]
        [MaxLength(500, ErrorMessage = "Comment can not be over 500 characters")]
        public string Body { get; set; } = string.Empty;
    }
}