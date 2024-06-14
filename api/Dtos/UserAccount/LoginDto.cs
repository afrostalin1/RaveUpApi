using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.UserAccount
{
    /// <summary>
    /// Data Transfer Object used to log the user in.
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string UserName { get; set;}

        [Required]
        public string Password { get; set;} 
    }
}