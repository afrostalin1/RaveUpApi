using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    /// <summary>
    /// Repository class for managing review comments.
    /// </summary>
    public class ReviewCommentRepository : IReviewCommentRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewCommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new review comment asynchronously.
        /// </summary>
        /// <param name="commentModel">The comment model to create.</param>
        /// <returns>The created review comment model.</returns>
        public async Task<ReviewComment> CreateAsync(ReviewComment commentModel)
        {
            await _context.ReviewComments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;

        }
        
        /// <summary>
        /// Deletes a review comment by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>The deleted review comment model, or null if not found.</returns>
        public async Task<ReviewComment?> DeleteAsync(int id)
        {
           var comment = await _context.ReviewComments.FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null)
            {
                return null;
            }

            _context.ReviewComments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        
        /// <summary>
        /// Retrieves all review comments asynchronously.
        /// </summary>
        /// <returns>A list of all review comments.</returns>
        public async Task<List<ReviewComment>> GetAllAsync()
        {
            return await _context.ReviewComments.Include(x => x.UserAccount).ToListAsync();
        }
        
        /// <summary>
        /// Retrieves a review comment by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The review comment model, or null if not found.</returns>
        public async Task<ReviewComment?> GetByIdAsync(int id)
        {
            return await _context.ReviewComments.Include(x => x.UserAccount).FirstOrDefaultAsync(c => c.Id == id);
        }
        
        /// <summary>
        /// Updates an existing review comment by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="commentModel">The updated comment model.</param>
        /// <returns>The updated review comment model, or null if not found.</returns>
        public async Task<ReviewComment?> UpdateAsync(int id, ReviewComment commentModel)
        {
            var existingComment = await _context.ReviewComments.Include(x => x.UserAccount).FirstOrDefaultAsync(c => c.Id == id);;
           
            if (existingComment == null)
            {
                return null;
            }

            existingComment.CommentBody = commentModel.CommentBody;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}