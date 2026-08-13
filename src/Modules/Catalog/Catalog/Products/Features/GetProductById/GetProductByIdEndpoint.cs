using Carter;
using Catalog.Contracts.Products.Dtos;
using Catalog.Contracts.Products.Features.GetProductById;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdRsponse(ProductDto Product);
    internal class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}",async(Guid id, ISender sender) =>
            {
                var result =await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdRsponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductId")
                .Produces<GetProductByIdEndpoint>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get product By Id")
                .WithDescription("Get Product By Id");
        }
    }
}
