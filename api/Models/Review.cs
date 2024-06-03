using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Venue { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string Body {get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public List<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

        


    
    }
}