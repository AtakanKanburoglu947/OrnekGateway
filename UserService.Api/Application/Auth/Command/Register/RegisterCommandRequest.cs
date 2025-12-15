using Authorization;
using Mediator;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Token;
using Utils;

namespace UserService.Api.Application.Auth.Command.Register
{
    public class RegisterCommandRequest : IRequest<Response>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterCommandRequestHandler(UserDbContext context, ITokenService tokenService)
        : IRequestHandler<RegisterCommandRequest, Response>
    {
        public async Task<Response> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {

            var user = new Entities.User { Email = request.Email, Name = request.Name, 
                Password = CryptoUtils.Hash(request.Password) };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            context.UserPermissions.Add(new Entities.UserPermission { PermissionId = ((int)UserClaimEnum.StandardUser), UserId = user.Id });
            await context.SaveChangesAsync();
            return Response.Success("Başarılı");

        }
    }
}
