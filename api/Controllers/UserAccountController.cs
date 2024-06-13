using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserAccount;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager;

        private readonly ITokenService _tokenService;

        private readonly SignInManager<UserAccount> _signInManager;

        public UserAccountController(UserManager<UserAccount> userManager, ITokenService tokenService, SignInManager<UserAccount> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// POST request method for user registration.
        /// Registers a new user with the provided username, email, and password.
        /// If successful, assigns the "User" role and returns a token for authentication.
        /// </summary>
        /// <param name="registerDto">Data transfer object containing registration details.</param>
        /// <returns>
        /// Returns <see cref="OkObjectResult"/> with a <see cref="NewUserDto"/> containing username, email, and authentication token if registration is successful.
        /// Returns <see cref="BadRequestObjectResult"/> if model state is invalid.
        /// Returns <see cref="StatusCodeResult"/> with status code 500 and error details if an error occurs during registration or role assignment.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userAccount = new UserAccount
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                //CreateAsync creates the user
                var createdUser = await _userManager.CreateAsync(userAccount, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(userAccount, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = userAccount.UserName,
                                Email = userAccount.Email,
                                Token = _tokenService.CreateToken(userAccount)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
        /// <summary>
        /// POST request method for user login.
        /// Attempts to authenticate a user with the provided username and password.
        /// If successful, returns a token for authentication.
        /// </summary>
        /// <param name="loginDto">Data transfer object containing login credentials.</param>
        /// <returns>
        /// Returns <see cref="OkObjectResult"/> with a <see cref="NewUserDto"/> containing username, email, and authentication token if login is successful.
        /// Returns <see cref="BadRequestObjectResult"/> if model state is invalid.
        /// Returns <see cref="UnauthorizedObjectResult"/> with an error message if username is not found or password is incorrect.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }
            //Lockout feature set to false, for ease of development
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }

            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );

        }
    }
}