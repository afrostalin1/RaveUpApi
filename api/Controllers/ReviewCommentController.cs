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

        //Putting :int means you constrain the datatype it takes to an int
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
        // :int is the route constraints 
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