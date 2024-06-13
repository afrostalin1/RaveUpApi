using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    /// <summary>
    /// Represents the database model of the 'Review Comment' table.
    /// A reviewComment is a user's comment on another users Review.
    /// To make foriegn keys, you set the field to what you want it to be and then use a navigation property to make the foreign key
    /// e.g. ReviewId is the column, and Review is the navigation property, same for UserAccountId and UserAccount
    /// </summary>
    public class ReviewComment
    {
        public int Id { get; set; }
        public int? ReviewId { get; set; }
        public Review? Review { get; set; }
        public string CommentBody { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}