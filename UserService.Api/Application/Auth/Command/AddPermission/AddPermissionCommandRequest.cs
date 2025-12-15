using Authorization;
using Mediator;
using MediatR;
using UserService.Api.Entities;

namespace UserService.Api.Application.Auth.Command.AddPermission
{
    [Authorize(UserClaimEnum.Admin)]
    public class AddPermissionCommandRequest : IRequest<Response>
    {
        public UserClaimEnum UserClaim { get; set; }
    }
    public class AddPermissionCommandRequestHandler(UserDbContext context)
        : IRequestHandler<AddPermissionCommandRequest, Response>
    {
        public async Task<Response> Handle(AddPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var permission = new Permission() { Name = request.UserClaim.ToString(), UserClaimEnum = request.UserClaim };
            if (Enum.IsDefined(request.UserClaim))
            {
                context.Permissions.Add(permission);
                await context.SaveChangesAsync();
                return Response.Success("Başarılı");
                
            }
            else
            {
                throw new Exception("Hata");
            }
            
        }
    }
}
