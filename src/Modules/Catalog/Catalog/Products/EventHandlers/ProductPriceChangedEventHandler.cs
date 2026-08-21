
using Catalog.Products.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(IBus bus,ILogger<ProductPriceChangedEvent> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Type:{DomainEvent}  Product price changed: {ProductId}, Name: {ProductName}, New Price: {ProductPrice}", notification.GetType().Name, notification.Product.Id, notification.Product.Name, notification.Product.Price);
            var integrationEvent = new ProductPriceChangedIntegrationEvent()
            {
                ProductId = notification.Product.Id,
                Name = notification.Product.Name,
                Price = notification.Product.Price,
                ImageFile = notification.Product.ImageFile,
                Category = notification.Product.Category,
                Description = notification.Product.Description
            };
            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
