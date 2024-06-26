using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    /// <summary>
    /// Represents the database model of the 'Review' table.
    /// A review is a user's assessment of an artist's performance.
    /// To make foriegn keys, you set the field to what you want it to be and then use a navigation property to make the foreign key
    /// e.g. UserAccountId is the column, and UserAccount is the navigation property.
    /// </summary>
    public class Review
    {
        public int Id { get; set; }

        public string Artist { get; set; } = string.Empty;

        public string Venue { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string ReviewBody { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

        public List<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();





    }
}