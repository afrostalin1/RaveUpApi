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