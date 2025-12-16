using Authorization;
using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace UserService.Api.Application.Auth.Query.GetPersonalDetails
{
    public class PersonalDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Claims { get; set; }
    }
    [Authorize(UserClaimEnum.StandardUser)]
    public class GetPersonalDetailsQuery : IRequest<Response<PersonalDetailsDto>>
    {

    }
    public class GetPersonalDetailsQueryHandler(UserDbContext context, IHttpContextAccessor httpContextAccessor) 
        : IRequestHandler<GetPersonalDetailsQuery, Response<PersonalDetailsDto>>
    {
        public async Task<Response<PersonalDetailsDto>> Handle(GetPersonalDetailsQuery request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Email == email);
         
            var userPermissions = context.UserPermissions.Where(x=> x.UserId == user.Id).Select(x=>x.Permission).Select(x=>x.Name);
            var personalDetails = new PersonalDetailsDto()
            {
                Id = user.Id,
                Claims = userPermissions.ToList(),
                Email = user.Email,
                Name = user.Name,
            };
            return  Response<PersonalDetailsDto>.Success("Başarılı",personalDetails);
          }
    }
}
