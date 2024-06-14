using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.UserAccount
{
    /// <summary>
    /// Data Transfer Object used to give a token to a new user as part of the registration process.
    /// </summary>
    public class NewUserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}