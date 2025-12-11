using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        public TokenService()
        {
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.Secret)),
            };
        }
        public string CreateToken(string email, IEnumerable<string>? permissions)
        {
            var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.Secret)),
                        SecurityAlgorithms.HmacSha256
                    );

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };
            if (permissions is not null)
            {
                foreach (var permission in permissions)
                {
                    claims.Add(new Claim(permission, "true"));
                }

            }
        

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: signingCredentials
            );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string? token)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var principal = jwtSecurityTokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token bulunamadı");
            return principal;
        }

        public async Task<bool> ValidateToken(string token)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationResult = await jwtSecurityTokenHandler.ValidateTokenAsync(token, _tokenValidationParameters);

            return tokenValidationResult.IsValid;
        }
    }
}
