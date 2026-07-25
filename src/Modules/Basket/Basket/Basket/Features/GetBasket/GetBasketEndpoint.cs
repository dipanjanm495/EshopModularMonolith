using Basket.Basket.Dtos;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketResponse(
        ShoppingCartDto ShoppingCartDto
    );

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                var query = new GetBasketQuery(username);
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }).Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .WithDescription("Get basket by username");
        }
    }
}
