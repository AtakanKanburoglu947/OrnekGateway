using Mediator;
using MediatR;

namespace UserService.Api.Application.Auth.Query.GetUser
{
    public class GetUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class GetUserQuery : IRequest<Response<GetUserDto>>
    {
        public int Id { get; set; }
    }
    public class GetUserQueryHandler(UserDbContext context) : IRequestHandler<GetUserQuery, Response<GetUserDto>>
    {
        public async Task<Response<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.Id);
            if (user is null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }
            else
            {
                var dto = new GetUserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                };
                return Response<GetUserDto>.Success("başarılı", dto);
            }
        }
    }
}
