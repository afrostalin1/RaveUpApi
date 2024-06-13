using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ReviewComment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using api.Extensions;

namespace api.Controllers
{
    /// <summary>
    /// Controller for the ReviewComment model. It has attributes that specifies its route and that its an ApiController
    /// </summary>
    [Route("api/comment")]
    public class ReviewCommentController : ControllerBase
    {
        private readonly IReviewCommentRepository _commentRepo;

        private readonly IReviewRepository _reviewRepo;

        private readonly UserManager<UserAccount> _userManager;

        public ReviewCommentController(IReviewCommentRepository commentRepo, IReviewRepository reviewRepository, UserManager<UserAccount> userManager)
        {
            _commentRepo = commentRepo;
            _reviewRepo = reviewRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// A Get request method that retrieves a collection of all ReviewComments.
        /// </summary>
        /// <returns><returns> 
        /// Returns a list of <see cref="ReviewCommentDto"/>s (Data Transfer Objects) of reviews if successful.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //The ModelState.IsValid statement enforces the attributes added to Dto's so that data validation is enforced
            //ModelState is inherited from the ControllerBase
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(s => s.ToReviewCommentDto());

            return Ok(commentDto);
        }

        /// <summary>
        /// A Get request method that retrieves a ReviewComment based on its ID.
        /// </summary>
        /// <param name="id">An int specifying the id of the review to be retrieved</param>
        /// <returns>
        /// Returns a <see cref="ReviewCommentDto"/> of the review if successful.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// Returns <see cref="NotFoundResult"/> if no review with the specified ID is found.
        /// </returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToReviewCommentDto());
        }

        /// <summary>
        /// A POST request that serves to create a ReviewComment and save it in the database
        /// It also takes the logged in user's username and adds that to the dto
        /// </summary>
        /// <param name="reviewId">An int specifying the id of the review to be retrieved.</param>
        /// <param name="commentDto">The data transfer object containing the ReviewComment details.</param>
        /// <returns>  
        /// Returns a <see cref="CreatedAtActionResult"/> indicating the result of the creation operation.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid or an error occurs during creation.
        /// </returns>
        [HttpPost("{reviewId:int}")]
        public async Task<IActionResult> Create([FromRoute] int reviewId, [FromBody] CreateReviewCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _reviewRepo.ReviewExists(reviewId))
            {
                return BadRequest("Review does not exist");
            }

            string username;
            try
            {
                username = User.GetUsername();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToReviewCommentFromCreate(reviewId);
            commentModel.UserAccountId = appUser.Id;

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToReviewCommentDto());

        }

        /// <summary>
        /// A PUT request that updates an existing ReviewComment
        /// </summary>
        /// <param name="id">An int specifying the id of the review to be updated</param>
        /// <param name="updateDto">The data transfer object containing the ReviewComment details that need to be updated</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the update operation.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// Returns <see cref="NotFoundResult"/> if the review with the specified ID is not found.
        /// Returns <see cref="OkObjectResult"/> with the updated review DTO if the update is successful.
        /// </returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToReviewCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToReviewCommentDto());
        }

        /// <summary>
        /// A DELETE request that deletes the specified ReviewComment
        /// </summary>
        /// <param name="id">An int specifying the id of the review to be retrieved</param>
        /// <returns> 
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// Returns <see cref="NotFoundResult"/> if the review with the specified ID is not found.
        /// Returns <see cref="NoContentResult"/> if the review is successfully deleted.
        /// </returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}