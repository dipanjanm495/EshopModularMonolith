using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers
{
    public class ProductCreatedEventHandler(ILogger<ProductCreatedEvent> logger) : INotificationHandler<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Type:{DomainEvent}  Product created: {ProductId}, Name: {ProductName}, Price: {ProductPrice}",notification.GetType().Name, notification.Product.Id, notification.Product.Name, notification.Product.Price);

            return Task.CompletedTask;
        }
    }
}
