using Authorization;
using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UserService.Api.Application.Auth.Command.RemoveUserPermission
{
    [Authorize(UserClaimEnum.Admin)]
    public class RemoveUserPermissonCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
    public class RemoveUserPermissonCommandHandler(UserDbContext userDbContext) : IRequestHandler<RemoveUserPermissonCommand, Response>
    {

        public async Task<Response> Handle(RemoveUserPermissonCommand request, CancellationToken cancellationToken)
        {
            var userPermissions = userDbContext.UserPermissions;
            var userPermisson = await userPermissions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.PermissionId == request.PermissionId);
            if (userPermisson != null) {
                userPermissions.Remove(userPermisson);
            
                await userDbContext.SaveChangesAsync();
                return Response.Success("Başarılı");
            }
            else
            {
                throw new Exception("Kullanıcıda belirtilen yetki bulunmuyor");
            }
        }
    }
}
