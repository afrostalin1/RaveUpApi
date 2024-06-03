using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ReviewComment;
using api.Models;

namespace api.Mappers
{
    public static class ReviewCommentMapper
    {
        public static ReviewCommentDto ToReviewCommentDto(this ReviewComment commentModel)
        {
            return new ReviewCommentDto
            {
                Id = commentModel.Id,
                Body = commentModel.Body,
                ReviewId = commentModel.ReviewId,
                CreatedBy = commentModel.UserAccount.UserName 

            };
        }

        public static ReviewComment ToReviewCommentFromCreate(this CreateReviewCommentDto commentDto, int reviewId)
        {
            return new ReviewComment
            {
                Body = commentDto.Body,
                ReviewId = reviewId

            };
        }

        
        public static ReviewComment ToReviewCommentFromUpdate(this UpdateReviewCommentRequestDto commentDto)
        {
            return new ReviewComment
            {
                Body = commentDto.Body,

            };
        }
    }
}