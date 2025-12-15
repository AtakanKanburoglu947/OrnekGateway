using Authorization;
using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace UserService.Api.Application.Auth.Command.ChangeUserEmail
{
    [Authorize(UserClaimEnum.StandardUser)]
    public class ChangeUserEmailCommandRequest : IRequest<Response>
    {
        public string NewEmail { get; set; }
    }
    public class ChangeUserEmailCommandRequestHandler(UserDbContext context,IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangeUserEmailCommandRequest, Response>
    {
        
        public async Task<Response> Handle(ChangeUserEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var users = context.Users;
            var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await users.FirstOrDefaultAsync(x => x.Email == email);
            if ( user != null && !users.Any(x => x.Email == request.NewEmail))
            {
              
                    user.Email = request.NewEmail;
                    await context.SaveChangesAsync();
                    return Response.Success("başarılı");
                
            }
            else
            {
                throw new Exception("Hata");
            }
              
        }
    }
}
