using Basket.Data;
using Basket.Data.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;

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

    internal class RemoveItemFromBasketHandler(IBasketRepository basketRepository) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetBasket(request.UserName,false,cancellationToken);  

            shoppingCart.RemoveItem(request.ProductId);

            await basketRepository.SaveChangeAsync(request.UserName,cancellationToken);

            return new RemoveItemFromBasketResult(shoppingCart.Id);
        }
    }
}