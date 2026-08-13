using Basket.Basket.Dtos;
using Basket.Data.Repository;
using Mapster;
using Shared.Contracts.CQRS;

namespace Basket.Basket.Features.GetBasket
{

    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.Username,true,cancellationToken);
            var shoppingCartDto = basket.Adapt<ShoppingCartDto>();
            return new GetBasketResult(shoppingCartDto);
        }
    }
}
