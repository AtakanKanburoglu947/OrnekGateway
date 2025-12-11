using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Token
{
    public interface ITokenService
    {
        string CreateToken(string email, IEnumerable<string>? permissions);
        ClaimsPrincipal GetClaimsPrincipalFromToken(string? token);
        Task<bool> ValidateToken(string token);
    }
}
