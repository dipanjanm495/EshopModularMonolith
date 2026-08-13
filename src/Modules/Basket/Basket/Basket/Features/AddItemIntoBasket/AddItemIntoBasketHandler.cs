using Basket.Basket.Dtos;
using Basket.Data;
using Basket.Data.Repository;
using Catalog.Contracts.Products.Features.GetProductById;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Basket.Features.AddItemIntoBasket
{

    public record AddItemIntoBasketCommand(
        string UserName,
        ShoppingCartItemDto ShoppingCartItemDto
    ) : ICommand<AddItemIntoBasketResult>;

    public record AddItemIntoBasketResult(
        Guid Id
    );

    public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemIntoBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.ShoppingCartItemDto.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.ShoppingCartItemDto.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }

    internal class AddItemIntoBasketHandler(IBasketRepository basketRepository,ISender sender) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetBasket(request.UserName,false, cancellationToken);

            var result = await sender.Send(new GetProductByIdQuery(request.ShoppingCartItemDto.ProductId));

            shoppingCart.AddItem(request.ShoppingCartItemDto.ProductId, request.ShoppingCartItemDto.Quantity, request.ShoppingCartItemDto.Color, result.Product.price, result.Product.Name);

            await basketRepository.SaveChangeAsync(request.UserName,cancellationToken);
            return new AddItemIntoBasketResult(shoppingCart.Id);
        }
    }
}
