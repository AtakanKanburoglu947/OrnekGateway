using Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Entities;

namespace ProductService.Api.Application.Query.GetProductsOfSeller
{
    public class GetProductsOfSellerCommandQuery : IRequest<Response<List<Product>>>
    {
        public int SellerId { get; set; }
    }
    public class GetProductsOfSellerCommandRequestHandler(ProductDbContext productDbContext) 
        : IRequestHandler<GetProductsOfSellerCommandQuery, Response<List<Product>>>
    {

        public async Task<Response<List<Product>>> Handle(GetProductsOfSellerCommandQuery request, CancellationToken cancellationToken)
        {
           var products = await productDbContext.Products.Where(x => x.SellerId == request.SellerId).ToListAsync();
            return Response<List<Product>>.Success("başarılı", products);
        }
    }
}
