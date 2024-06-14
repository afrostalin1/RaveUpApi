using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    /// <summary>
    /// Interface defining methods the Token Service should implement
    /// </summary>
    public interface ITokenService
    {
        string CreateToken(UserAccount userAccount);
    }
}