using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.Helpers;
using api.Models;

namespace api.Repository
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync(QueryObject query);

        Task<Review?> GetByIdAsync(int id); //Finds by id, Review is ? because it might not find anything

        Task<Review> CreateAsync(Review reviewModel); // Takes in review and is a review entity

        Task<Review?> UpdateAsync(int id, UpdateReviewRequestDto updateReviewRequestDto);

        Task<Review?> DeleteAsync(int id);
        // a task that checks to see if a review exists
        Task<bool> ReviewExists(int id);
    }
}