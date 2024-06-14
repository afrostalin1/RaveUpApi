using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ReviewComment;
// A reason to make dto's is to put the data validation here instead of the Model. If put in the model it'll apply it globally. 
namespace api.Dtos.Review
{
    /// <summary>
    /// Data Transfer Object representing a Review
    /// </summary>
    public class ReviewDto
    {
        public int Id { get; set; }

        public string Artist { get; set; } = string.Empty;

        public string Venue { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string ReviewBody { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public List<ReviewCommentDto> ReviewComments { get; set; }
    }
}