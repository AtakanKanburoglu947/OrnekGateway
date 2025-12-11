using Authorization;
using MediatR;

namespace WeatherAPI.Application.Query
{
    [Authorize(UserClaimEnum.StandardUser)]
    public class GetWeatherForecastQueryRequest : IRequest<List<WeatherForecast>>
    {

    }
    public class GetWeatherForecastQueryRequestHandler : IRequestHandler<GetWeatherForecastQueryRequest, List<WeatherForecast>>
    {
        public async Task<List<WeatherForecast>> Handle(GetWeatherForecastQueryRequest request, CancellationToken cancellationToken)
        {
            string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var weatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();
            var result = await Task.FromResult(weatherForecast);
            return result;
        }
    }
}
