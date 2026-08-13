using Basket.Basket.Dtos;
using Basket.Basket.Models;
using Basket.Data.Repository;
using FluentValidation;
using Shared.Contracts.CQRS;

namespace Basket.Basket.Features.CreateBasket
{ 
    public record CreateBasketCommand(
        ShoppingCartDto ShoppingCart
    ): ICommand<CreateBasketResult>;

    public record CreateBasketResult(
        Guid Id
    );

    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart.Username).NotEmpty().WithMessage("UserName is required.");
        }
    }
    internal class CreateBasketHandler(IBasketRepository basketRepository) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = CreateNewBasket(command.ShoppingCart);
            await basketRepository.CreateBasket(shoppingCart,cancellationToken);
            return new CreateBasketResult(shoppingCart.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCart)
        {
            var newBasket = ShoppingCart.Create(new Guid(), shoppingCart.Username);

            foreach (var item in shoppingCart.Items)
            {
                newBasket.AddItem(item.ProductId, item.Quantity, item.Color, item.Price, item.ProductName);
            }
            return newBasket;
        }
    }
}
