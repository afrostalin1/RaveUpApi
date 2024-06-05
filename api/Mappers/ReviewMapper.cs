using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.Models;

namespace api.Mappers
{
    public static class ReviewMapper
    {
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