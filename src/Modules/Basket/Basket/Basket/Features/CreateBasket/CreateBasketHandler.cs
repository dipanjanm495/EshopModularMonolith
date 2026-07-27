using Basket.Basket.Dtos;
using Basket.Basket.Models;
using Basket.Data;
using FluentValidation;
using Shared.CQRS;

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
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
    internal class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = CreateNewBasket(command.ShoppingCart);

            dbContext.ShoppingCarts.Add(shoppingCart);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateBasketResult(shoppingCart.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCart)
        {
            var newBasket = ShoppingCart.Create(new Guid(), shoppingCart.UserName);

            foreach (var item in shoppingCart.Items)
            {
                newBasket.AddItem(item.ProductId, item.Quantity, item.Color, item.Price, item.ProductName);
            }
            return newBasket;
        }
    }
}
