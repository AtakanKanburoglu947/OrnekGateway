using Authorization;
using Mediator;
using MediatR;
using UserService.Api.Entities;

namespace UserService.Api.Application.Auth.Command.AddUserPermission
{
    [Authorize(UserClaimEnum.Admin)]
    public class AddUserPermissionCommandRequest : IRequest<Response>
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
    public class AddUserPermissionCommandRequestHandler(UserDbContext context)
        : IRequestHandler<AddUserPermissionCommandRequest, Response>
    {
        public async Task<Response> Handle(AddUserPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var userPermission = new UserPermission() { UserId = request.UserId, PermissionId = request.PermissionId };
            context.UserPermissions.Add(userPermission);
            await context.SaveChangesAsync();
            return Response.Success("Başarılı");
        }
    }
}
