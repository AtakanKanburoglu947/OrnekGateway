using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Application.Command.AddProduct;
using ProductService.Api.Application.Query.GetProductsOfSeller;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsOfSeller(int sellerId)
        {
            var response = await mediator.Send(new GetProductsOfSellerCommandQuery() { SellerId = sellerId});
            return Ok(response);
        }
    }
}
