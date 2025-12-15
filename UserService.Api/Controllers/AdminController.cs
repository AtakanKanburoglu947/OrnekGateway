using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Application.Auth.Command.AddPermission;
using UserService.Api.Application.Auth.Command.AddUserPermission;
using UserService.Api.Application.Auth.Command.RemoveUserPermission;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;
        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
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
        [HttpDelete]
        public async Task<IActionResult> RemoveUserPermission(RemoveUserPermissonCommand request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
