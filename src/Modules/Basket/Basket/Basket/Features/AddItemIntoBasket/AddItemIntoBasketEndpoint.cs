using Basket.Basket.Dtos;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Basket.Basket.Features.AddItemIntoBasket
{

    public record AddItemIntoBasketRequest(
        string Username,
        ShoppingCartItemDto ShoppingCartItemDto
    );

    public record AddItemIntoBasketResponse(
       Guid id);

    public class AddItemIntoBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}/items", async ([FromRoute]string userName,AddItemIntoBasketRequest request, ISender sender) =>
            {
                var command = new AddItemIntoBasketCommand(userName, request.ShoppingCartItemDto);
                var result = await sender.Send(command);
                var response = result.Adapt<AddItemIntoBasketResponse>();
                return Results.Created($"Item added in {response.id}",response);
            }).Produces<AddItemIntoBasketResponse>(StatusCodes.Status200OK)
            .WithDescription("Add item into basket");
        }
    }
}
