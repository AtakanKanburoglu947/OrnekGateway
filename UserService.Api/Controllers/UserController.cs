using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Application.Auth.Command.AddPermission;
using UserService.Api.Application.Auth.Command.AddUserPermission;
using UserService.Api.Application.Auth.Command.ChangeUserEmail;
using UserService.Api.Application.Auth.Command.Login;
using UserService.Api.Application.Auth.Command.Register;
using UserService.Api.Application.Auth.Query.GetPersonalDetails;
using UserService.Api.Application.Auth.Query.GetUser;

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
        public async Task<IActionResult> ChangeUserEmail(ChangeUserEmailCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonalDetails()
        {
            var response = await mediator.Send(new GetPersonalDetailsQuery());
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await mediator.Send(new GetUserQuery() { Id = id});
            return Ok(response);
        }
     
    

    }
}

