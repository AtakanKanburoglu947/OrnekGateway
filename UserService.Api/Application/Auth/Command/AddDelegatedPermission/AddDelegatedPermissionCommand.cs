using Authorization;
using Mediator;
using MediatR;
using UserService.Api.Entities;

namespace UserService.Api.Application.Auth.Command.AddDelegatedPermission
{
    [Authorize(UserClaimEnum.Admin)]
    public class AddDelegatedPermissionCommand : IRequest<Response>
    {
       public int UserId { get; set; }
       public int PermissionId { get; set; }
    }
    public class AddDelegatedPermissionCommandHandler(UserDbContext context)
        : IRequestHandler<AddDelegatedPermissionCommand, Response>
    {
        
        public async Task<Response> Handle(AddDelegatedPermissionCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.UserId) ?? throw new Exception("Kullanıcı bulunamadı");
            var permission = await context.Permissions.FindAsync(request.PermissionId) ?? throw new Exception("Yetki bulunamadı");
            var delegatedPermission = new DelegatedPermission() { UserId = user.Id, PermissionId = permission.Id, 
                ExpiresAt = DateTime.Now.AddDays(7) };
            context.DelegatedPermissions.Add(delegatedPermission);
            await context.SaveChangesAsync();
            return Response.Success("başarılı");
        }
    }
}
