using Authorization;
using Mediator;
using MediatR;
using ProductService.Api.Entities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductService.Api.Application.Command.AddProduct
{
    [Authorize(UserClaimEnum.StandardUser)]
    public class AddProductCommandRequest : IRequest<Response>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
    public class AddProductCommandRequestHandler(IHttpContextAccessor httpContextAccessor,HttpClient httpClient
        , ProductDbContext productDbContext) : IRequestHandler<AddProductCommandRequest, Response>
    {

        public async Task<Response> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var authHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5170/api/User/GetPersonalDetails");
            httpRequest.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
            var response = await httpClient.SendAsync(httpRequest, cancellationToken);
            response.EnsureSuccessStatusCode();
            var userJson = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument .Parse(userJson);
            var userId = doc.RootElement.GetProperty("data").GetProperty("id").GetInt32();
            var product = new Product { Name = request.Name, 
                Description = request.Description,
                SellerId = userId,
                CategoryId = request.CategoryId
            };
            productDbContext.Products.Add(product);
            await productDbContext.SaveChangesAsync();
            return Response.Success("Başarılı");
        }
    }
}
