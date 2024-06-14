using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Review;
using api.Helpers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    /// <summary>
    /// Repository class for managing reviews.
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new review asynchronously.
        /// </summary>
        /// <param name="reviewModel">The review model to create.</param>
        /// <returns>The created review model.</returns>
        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        /// <summary>
        /// Deletes a review by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <returns>The deleted review model, or null if not found.</returns>
        public async Task<Review?> DeleteAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (reviewModel == null)
            {
                return null;
            }

            _context.Reviews.Remove(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }
        
        /// <summary>
        /// Retrieves all reviews asynchronously based on the specified query object.
        /// </summary>
        /// <param name="query">The query object containing filter and sorting options.</param>
        /// <returns>A list of reviews matching the query criteria.</returns>
        public async Task<List<Review>> GetAllAsync(QueryObject query)
        {
            // The 'include' keyword allows us to map the reviewcomments to the reviews when calling the method to get reviews.
            //The AsQueryable() makes the results queryable
            var reviews = _context.Reviews.Include(c => c.ReviewComments).ThenInclude(a => a.UserAccount).AsQueryable();


            //Each of these if statements applies the 
            if (!string.IsNullOrWhiteSpace(query.Artist))
            {
                reviews = reviews.Where(s => s.Artist.Contains(query.Artist));
            }

            if (!string.IsNullOrWhiteSpace(query.Venue))
            {
                reviews = reviews.Where(s => s.Venue.Contains(query.Venue));
            }

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                reviews = reviews.Where(s => s.Genre.Contains(query.Genre));
            }
            // This if allows the 'sort by' field to work, 
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    // : is a ternary operator saying this will happen if its false
                    reviews = query.IsDescending ? reviews.OrderByDescending(s => s.Artist) : reviews.OrderBy(s => s.Artist);
                }

                if (query.SortBy.Equals("Venue", StringComparison.OrdinalIgnoreCase))
                {
                    reviews = query.IsDescending ? reviews.OrderByDescending(s => s.Venue) : reviews.OrderBy(s => s.Venue);
                }

                if (query.SortBy.Equals("Genre", StringComparison.OrdinalIgnoreCase))
                {
                    reviews = query.IsDescending ? reviews.OrderByDescending(s => s.Genre) : reviews.OrderBy(s => s.Genre);
                }

            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            // ToListAsync() needs to be done at the end before any filtering because it triggers the sql being sent
            // Take(x) will 'take' x elements of a set, Skip(y) will skip the 1st y elements of a set and leave the rest.
            //Combining these methods lets you paginate your results so you dotn return them all at the same time
            return await reviews.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        /// <summary>
        /// Retrieves a review by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the review to retrieve.</param>
        /// <returns>The review model, or null if not found.</returns>
        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(c => c.ReviewComments).FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <summary>
        /// Checks if a review exists by ID.
        /// </summary>
        /// <param name="id">The ID of the review to check.</param>
        /// <returns>True if the review exists, otherwise false.</returns>
        public Task<bool> ReviewExists(int id)
        {
            return _context.Reviews.AnyAsync(s => s.Id == id);
        }
        
        /// <summary>
        /// Updates an existing review by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="updateReviewRequestDto">The DTO containing updated review data.</param>
        /// <returns>The updated review model, or null if not found.</returns>
        public async Task<Review?> UpdateAsync(int id, UpdateReviewRequestDto updateReviewRequestDto)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (existingReview == null)
            {
                return null;
            }

            existingReview.Artist = updateReviewRequestDto.Artist;
            existingReview.Venue = updateReviewRequestDto.Venue;
            existingReview.Genre = updateReviewRequestDto.Genre;
            existingReview.Rating = updateReviewRequestDto.Rating;
            existingReview.ReviewBody = updateReviewRequestDto.ReviewBody;

            await _context.SaveChangesAsync();

            return existingReview;
        }
    }
}