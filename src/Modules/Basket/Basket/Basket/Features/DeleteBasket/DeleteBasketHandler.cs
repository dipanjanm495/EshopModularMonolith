using Basket.Data;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;

namespace Basket.Basket.Features.DeleteBasket
{

    public record DeleteBasketCommand(
        string UserName
    ) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(
        bool IsSuccess
    );

    internal class DeleteBasketHandler(BasketDbContext dbContext) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts.SingleOrDefaultAsync(x=>x.Username.Equals(request.UserName),cancellationToken);
            if (basket == null)
            {
               throw new ArgumentNullException(request.UserName);
            }
            dbContext.ShoppingCarts.Remove(basket);
            await dbContext.SaveChangesAsync();
            return new DeleteBasketResult(true);
        }
    }
}
