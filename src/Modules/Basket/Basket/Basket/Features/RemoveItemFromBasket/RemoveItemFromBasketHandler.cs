using Basket.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(
        string UserName,
        Guid ProductId
    ) : ICommand<RemoveItemFromBasketResult>;

    public record RemoveItemFromBasketResult(
        Guid Id
    );

    public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
    {
        public RemoveItemFromBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        }
    }

    internal class RemoveItemFromBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketDbContext.ShoppingCarts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Username == request.UserName, cancellationToken);

            ArgumentNullException.ThrowIfNull(shoppingCart, nameof(shoppingCart));

            shoppingCart.RemoveItem(request.ProductId);

            await basketDbContext.SaveChangesAsync(cancellationToken);

            return new RemoveItemFromBasketResult(shoppingCart.Id);
        }
    }
}