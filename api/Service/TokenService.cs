using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    /// <summary>
    /// Service class for generating JWT tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="config">The configuration settings.</param>
        public TokenService(IConfiguration config)
        {
            _config = config;
            //Symmetricseucritykey will encript it in a unique way so that only oru server can recongize the token
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        /// <summary>
        /// Creates a JWT token for the specified user account.
        /// </summary>
        /// <param name="userAccount">The user account for which to create the token.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string CreateToken(UserAccount userAccount)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, userAccount.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, userAccount.UserName)
            };
            //This is the signin credentials e.g. the type of encryption
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}