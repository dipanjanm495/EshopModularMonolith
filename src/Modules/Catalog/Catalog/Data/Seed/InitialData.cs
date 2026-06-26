using Catalog.Products.Models;

namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static readonly List<Product> Products = new List<Product>
        {
            Product.Create(new Guid("11111111-1111-1111-1111-111111111111"), "Product 1", "Description for Product 1", "product1.jpg", 10.99m, new List<string> { "Category A" }),
            Product.Create(new Guid("22222222-2222-2222-2222-222222222222"), "Product 2", "Description for Product 2", "product2.jpg", 19.99m, new List<string> { "Category B" }),
            Product.Create(new Guid("33333333-3333-3333-3333-333333333333"), "Product 3", "Description for Product 3", "product3.jpg", 5.99m, new List<string> { "Category A" })
        };
    }
}
