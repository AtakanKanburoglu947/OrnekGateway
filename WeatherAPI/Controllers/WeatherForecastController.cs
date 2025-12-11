using Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Application.Query;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IMediator mediator) : ControllerBase
    {
     
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var response = await mediator.Send(new GetWeatherForecastQueryRequest());
            return Ok(response);
        }
    }
}
