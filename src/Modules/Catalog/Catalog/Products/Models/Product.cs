using Catalog.Products.Events;
using Shared.DDD;
namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;
        public List<string> Category { get; private set; } = new List<string>();

        public string ImageFile { get; private set; } = default!;
        public decimal Price { get; private set; }

        public static Product Create(Guid id, string name, string description, string imageFile, decimal price, List<string> category)
        {
            var product= new Product
            {
                Id = id,
                Name = name,
                Description = description,
                Category = category,
                ImageFile = imageFile,
                Price = price,
                CreatedAt = DateTime.UtcNow
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

           return product;
        }
        
        public void Update(string name, string description, List<string> category, string imageFile, decimal price)
        {
            Name = name;
            Description = description;
            Category = category;
            ImageFile = imageFile;
            Price = price;
            LastModified = DateTime.UtcNow;


            if(Price != price)
            {
                AddDomainEvent(new ProductPriceChangedEvent(this));
            }
        }

    }
}
