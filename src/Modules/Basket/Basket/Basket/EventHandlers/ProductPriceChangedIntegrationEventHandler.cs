using Basket.Basket.Features.UpdateItemPriceInBasket;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler(ISender sender,ILogger<ProductPriceChangedIntegrationEventHandler> logger) : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
           var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);
           var result = await sender.Send(command);

            if(result.Success)
            {
                logger.LogInformation("Product price updated in basket for ProductId: {ProductId}", context.Message.ProductId);
            }
            else
            {
                logger.LogError("Failed to update product price in basket for ProductId: {ProductId}", context.Message.ProductId);
            }

        }
    }
}
