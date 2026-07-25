using Basket.Basket.Dtos;
using Basket.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
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

    internal class AddItemIntoBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart =await basketDbContext.ShoppingCarts.
                Include(x => x.Items).
                SingleOrDefaultAsync(x => x.Username == request.UserName,cancellationToken);

            shoppingCart.AddItem(request.ShoppingCartItemDto.ProductId, request.ShoppingCartItemDto.Quantity, request.ShoppingCartItemDto.Color, request.ShoppingCartItemDto.Price, request.ShoppingCartItemDto.ProductName);

            await basketDbContext.SaveChangesAsync(cancellationToken);
            return new AddItemIntoBasketResult(shoppingCart.Id);
        }
    }
}
