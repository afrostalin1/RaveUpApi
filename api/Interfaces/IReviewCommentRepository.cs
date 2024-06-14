using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    /// <summary>
    /// Interface defining methods the ReviewComment repository should implement
    /// </summary>
    public interface IReviewCommentRepository
    {
        Task<List<ReviewComment>> GetAllAsync();

        Task<ReviewComment> GetByIdAsync(int id);

        Task<ReviewComment> CreateAsync(ReviewComment commentModel);

        Task<ReviewComment?> UpdateAsync(int id, ReviewComment commentModel);
        
        Task<ReviewComment?> DeleteAsync(int id);
    }
}