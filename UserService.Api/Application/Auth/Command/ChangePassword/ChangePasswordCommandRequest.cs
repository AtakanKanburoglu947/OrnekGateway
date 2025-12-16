using Authorization;
using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Utils;

namespace UserService.Api.Application.Auth.Command.ChangePassword
{
    [Authorize(UserClaimEnum.StandardUser)]
    public class ChangePasswordCommandRequest : IRequest<Response>
    {
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
    public class ChangePasswordCommandRequestHandler(UserDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangePasswordCommandRequest, Response>
    {
        public async Task<Response> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var email = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (request.NewPassword == request.NewPasswordConfirm) {
                user.Password = CryptoUtils.Hash(request.NewPassword);
                await context.SaveChangesAsync();
                return Response.Success("başarılı");
            }
            else
            {

                throw new Exception("Şifreler aynı olmalı");
            }
        }
    }
}
