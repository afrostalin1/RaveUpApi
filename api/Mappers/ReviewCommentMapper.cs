using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ReviewComment;
using api.Models;

namespace api.Mappers
{
    /// <summary>
    /// Static class for methods mapping the DTO's to the Model for ReviewComments and vice versa.
    /// </summary>
    public static class ReviewCommentMapper
    {
        /// <summary>
        /// Converts a ReviewComment Model to a Review DTO
        /// </summary>
        /// <param name="commentModel">The database Model for a ReviewComment</param>
        /// <returns>A ReviewComment DTO</returns>
        public static ReviewCommentDto ToReviewCommentDto(this ReviewComment commentModel)
        {
            return new ReviewCommentDto
            {
                Id = commentModel.Id,
                CommentBody = commentModel.CommentBody,
                ReviewId = commentModel.ReviewId,
                CreatedBy = commentModel.UserAccount?.UserName, 
                CreatedOn = commentModel.CreatedOn
            };
        }
        
        /// <summary>
        /// Converts a CreateReviewCommentDto to a ReviewComment model entry 
        /// </summary>
        /// <param name="commentDto">ReviewComment DTO used to create a review</param>
        /// <param name="reviewId">An int for the ID of the review the ReviewComment will be linked to</param>
        /// <returns>A ReviewComment Model</returns>
        public static ReviewComment ToReviewCommentFromCreate(this CreateReviewCommentDto commentDto, int reviewId)
        {
            return new ReviewComment
            {
                CommentBody = commentDto.CommentBody,
                ReviewId = reviewId
            };
        }

        /// <summary>
        /// Converts a CreateReviewCommentDto to a ReviewComment model entry 
        /// </summary>
        /// <param name="commentDto"></param>
        /// <returns>ReviewComment Model</returns>
        public static ReviewComment ToReviewCommentFromUpdate(this UpdateReviewCommentRequestDto commentDto)
        {
            return new ReviewComment
            {
                CommentBody = commentDto.CommentBody,
            };
        }
    }
}