using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.Models;

namespace api.Mappers
{
    /// <summary>
    /// Static class for methods mapping the DTO's to the Model for Reviews and vice versa.
    /// </summary>
    public static class ReviewMapper
    {
        /// <summary>
        /// Converts a Review Model to a Review DTO
        /// </summary>
        /// <param name="reviewModel">The database Model for a Review</param>
        /// <returns>A Review DTO</returns>
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                Id = reviewModel.Id,
                Artist = reviewModel.Artist,
                Venue = reviewModel.Venue,
                Genre = reviewModel.Genre,
                Rating = reviewModel.Rating,
                ReviewBody = reviewModel.ReviewBody,
                CreatedBy = reviewModel.UserAccount?.UserName,
                CreatedOn = reviewModel.CreatedOn,
                //This is mapping the comments in a list since Reviews and reviewcomments are in a one to many relationship
                ReviewComments = reviewModel.ReviewComments.Select(c => c.ToReviewCommentDto()).ToList() 
            };
        }
        
        /// <summary>
        /// Converts a CreateReviewRequestDto to a Review Model used in the HTTPPOST method for Reviews.
        /// </summary>
        /// <param name="reviewDto">A DTO representing a review</param>
        /// <returns>A Review model</returns>
        public static Review ToReviewFromCreateDto(this CreateReviewRequestDto reviewDto)
        {
            return new Review
            {
                Artist = reviewDto.Artist,
                Venue = reviewDto.Venue,
                Genre = reviewDto.Genre,
                Rating = reviewDto.Rating,
                ReviewBody = reviewDto.ReviewBody
            };
        }
    }
}