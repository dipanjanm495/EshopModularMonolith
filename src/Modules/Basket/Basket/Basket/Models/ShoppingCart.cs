using Shared.DDD;

namespace Basket.Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
        public string Username { get; private set; } = default!;

        private readonly List<ShoppingCartItem> _items = new List<ShoppingCartItem>();

        public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

        public decimal TotalPrice => _items.Sum(item => item.Price * item.Quantity);

        public static ShoppingCart Create(Guid id,string username)
        {
            ArgumentException.ThrowIfNullOrEmpty(username);
            var shoppingCart = new ShoppingCart
            {
                Id = id,
                Username = username
            };
            return shoppingCart;
        }

        public void AddItem(Guid productId, int quantity, string color, decimal price, string productName)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItem = Items.FirstOrDefault(item => item.ProductId == productId && item.Color == color);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;  
            }
            else
            {
                var newItem = new ShoppingCartItem(Id, productId, quantity, color, price, productName);
                _items.Add(newItem);
            }
        }

        public void RemoveItem(Guid productId)
        {
            var itemToRemove = Items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
            }
        }
    }
}
