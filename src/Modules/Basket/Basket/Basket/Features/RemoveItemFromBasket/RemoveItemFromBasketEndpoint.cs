using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketResponse(Guid Id );
    public class RemoveItemFromBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}/items/{productId}", async (string userName, Guid productId, ISender sender) =>
            {
                var command = new RemoveItemFromBasketCommand(userName, productId);
                var result = await sender.Send(command);
                var response = result.Adapt<RemoveItemFromBasketResponse>();
                return Results.Ok(response);
            }).Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK);
        }
    }
}
