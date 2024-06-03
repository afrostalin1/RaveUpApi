using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    //IdentityUser is inherited from because it forms the base class for a user but you can add to it
    public class UserAccount : IdentityUser
    {
        
    }
}