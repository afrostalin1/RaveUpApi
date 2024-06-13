using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    /// <summary>
    /// ClaimsExtensions contains extension methods for ClaimsPrincipal
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// Retrieves the logged-in user's username from their claims.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal used for the logged in user</param>
        /// <returns>The username from the claim if found; otherwise, an exception is thrown</returns>
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));
            if (claim == null)
            {
                throw new InvalidOperationException("Given name claim not found");
            }
            return claim.Value;
        }
    }

}