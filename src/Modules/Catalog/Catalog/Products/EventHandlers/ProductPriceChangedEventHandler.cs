
using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEvent> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Type:{DomainEvent}  Product price changed: {ProductId}, Name: {ProductName}, New Price: {ProductPrice}", notification.GetType().Name, notification.Product.Id, notification.Product.Name, notification.Product.Price);
            return Task.CompletedTask;
        }
    }
}
