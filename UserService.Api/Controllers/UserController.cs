using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Application.Auth.Command.AddPermission;
using UserService.Api.Application.Auth.Command.AddUserPermission;
using UserService.Api.Application.Auth.Command.Login;
using UserService.Api.Application.Auth.Command.Register;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);   
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddPermission(AddPermissionCommandRequest request)
        {
           var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserPermission(AddUserPermissionCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

    }
}
