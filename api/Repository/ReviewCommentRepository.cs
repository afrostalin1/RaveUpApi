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
    public class ReviewCommentRepository : IReviewCommentRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewCommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ReviewComment> CreateAsync(ReviewComment commentModel)
        {
            await _context.ReviewComments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;

        }

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

        public async Task<List<ReviewComment>> GetAllAsync()
        {
            return await _context.ReviewComments.Include(x => x.UserAccount).ToListAsync();
        }

        public async Task<ReviewComment?> GetByIdAsync(int id)
        {
            return await _context.ReviewComments.Include(x => x.UserAccount).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ReviewComment?> UpdateAsync(int id, ReviewComment commentModel)
        {
            var existingComment = await _context.ReviewComments.FindAsync(id);
           
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Body = commentModel.Body;

            await _context.SaveChangesAsync();


            return existingComment;
        }
    }
}