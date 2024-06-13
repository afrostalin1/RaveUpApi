using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Review;
using api.Extensions;
using api.Helpers;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//Want to filter by name of reviews
namespace api.Controllers
{
    /// <summary>
    /// Controller for the Review model. It has attributes that specifies its route and that its an ApiController
    /// </summary>
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewRepository _repository;

        private readonly UserManager<UserAccount> _userManager;

        public ReviewController(ApplicationDBContext context, IReviewRepository repository, UserManager<UserAccount> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        /// <summary>
        /// A Get request method that retrieves a collection of all reviews based on the specified query parameters.
        /// Requires the user to be authorized
        /// </summary>
        /// <param name="query">The query parameters used to filter and paginate the reviews. Can be null</param>
        /// <returns> 
        /// Returns a list of <see cref="ReviewDto"/>s (Data Transfer Objects) of reviews if successful.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// </returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviews = await _repository.GetAllAsync(query);
            var reviewDto = reviews.Select(s => s.ToReviewDto()).ToList();

            return Ok(reviewDto);
        }

        /// <summary>
        /// A Get request method that retrieves a review based on its ID.
        /// </summary>
        /// <param name="id">An int specifying the id of the review to be retrieved</param>
        /// <returns>
        /// Returns a <see cref="ReviewDto"/> of the review if successful.
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

            var review = await _repository.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        /// <summary>
        /// A POST request that serves to create a review and save it in the database
        /// It also takes the logged in user's username and adds that to the dto
        /// </summary>
        /// <param name="reviewDto">The data transfer object containing the review details.</param>
        /// <returns>  
        /// Returns a <see cref="CreatedAtActionResult"/> indicating the result of the creation operation.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid or an error occurs during creation.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequestDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            var reviewModel = reviewDto.ToReviewFromCreateDto();
            reviewModel.UserAccountId = appUser.Id;
            await _repository.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById), new { id = reviewModel.Id }, reviewModel.ToReviewDto());
        }

        /// <summary>
        /// A PUT request that updates an existing review
        /// </summary>
        /// <param name="id">An int specifying the id of the review to be updated</param>
        /// <param name="updateReviewRequestDto">The data transfer object containing the review details that need to be updated</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the update operation.
        /// Returns <see cref="BadRequestObjectResult"/> if the model state is invalid.
        /// Returns <see cref="NotFoundResult"/> if the review with the specified ID is not found.
        /// Returns <see cref="OkObjectResult"/> with the updated review DTO if the update is successful.
        /// </returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewRequestDto updateReviewRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewModel = await _repository.UpdateAsync(id, updateReviewRequestDto);

            if (reviewModel == null)
            {
                return NotFound();
            }

            return Ok(reviewModel.ToReviewDto());
        }

        /// <summary>
        /// A DELETE request that deletes the specified review
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

            var reviewModel = await _repository.DeleteAsync(id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }



    }
}