using Basket.Basket.Dtos;
using Basket.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;

namespace Basket.Basket.Features.GetBasket
{

    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler(BasketDbContext basketDbContext) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
           var basket = await basketDbContext.ShoppingCarts.AsNoTracking()
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Username == query.Username, cancellationToken);
            
            if(basket is null)
            {
                throw new ArgumentNullException($"Basket not found for username: {query.Username}");
            }
            var shoppingCartDto = basket.Adapt<ShoppingCartDto>();
            return new GetBasketResult(shoppingCartDto);
        }
    }
}
