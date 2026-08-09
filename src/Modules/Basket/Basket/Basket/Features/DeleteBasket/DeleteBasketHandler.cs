using Basket.Data;
using Basket.Data.Repository;
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

    internal class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.DeleteBasket(request.UserName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
