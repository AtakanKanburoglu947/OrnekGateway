using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Token;

namespace Authorization
{
    public class AuthorizationBehaviour<TRequest, TResponse>(
     IHttpContextAccessor httpContextAccessor,
     ITokenService tokenService
     ) : IPipelineBehavior<TRequest, TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttribute = request.GetType().GetCustomAttribute<AuthorizeAttribute>();
            
            if (authorizeAttribute != null)
            {
                var httpContext = httpContextAccessor.HttpContext;
                var token = httpContext.Request.Headers["Authorization"]
                    .ToString().Replace("Bearer ","",StringComparison.OrdinalIgnoreCase).Trim();
                var isTokenValid = await tokenService.ValidateToken(token);
                if (isTokenValid)
                {
                    var claimsPrincipal = tokenService.GetClaimsPrincipalFromToken(token);
                    httpContext.User = claimsPrincipal;
                    var userClaims = claimsPrincipal.Claims.Select(x=>x.Type);
                    var requiredClaims = authorizeAttribute.UserClaimEnums.Select(x=>x.ToString());
                    if (!userClaims.Any(x=>requiredClaims.Contains(x)))
                    {
                        throw new Exception("Yetki yok");
                    }
                    
                }
                else { throw new Exception("Yetki yok"); }
             
            }
            return await next();
        }

    }
}
