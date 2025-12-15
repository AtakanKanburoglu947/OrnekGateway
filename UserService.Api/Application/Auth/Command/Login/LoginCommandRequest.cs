using Authorization;
using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Token;
using UserService.Api.Entities;
using Utils;

namespace UserService.Api.Application.Auth.Command.Login
{
    public class LoginCommandRequest : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommandRequestHandler(UserDbContext userDbContext, ITokenService tokenService) : 
        IRequestHandler<LoginCommandRequest, Response<string>>
    {
        public async Task<Response<string>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var users = userDbContext.Users;
            var user = await users.FirstOrDefaultAsync(x=>x.Email == request.Email);
            if (user is not null && CryptoUtils.VerifyHash(request.Password,user.Password))
            { 
                var permissionIds = userDbContext.UserPermissions.Where(x=>x.UserId == user.Id).Select(x=>x.PermissionId);
                if (permissionIds.Any()) {
                    var permissions = userDbContext.Permissions.Where(x => permissionIds.Contains(x.Id));  
                    var permissionNames = permissions.Select(x => x.Name);   
                    var token = tokenService.CreateToken(user.Email,permissionNames);
                    return Response<string>.Success("Başarılı", token);
                }
                else
                {
                    var token = tokenService.CreateToken(user.Email, null);
                    return Response<string>.Success("Başarılı", token);

                }
            }
            else
            {
                throw new Exception("Kullanıcı kayıtı bulunamadı");
            }
            
        }
    }
}
