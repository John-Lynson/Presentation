using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; private set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var cartItem = CartItems
                .FirstOrDefault(i => i.Product.Id == product.Id);

            if (cartItem != null)
            {
                var newCartItem = new CartItem.Builder()
                    .WithProduct(cartItem.Product)
                    .WithQuantity(cartItem.Quantity + quantity)
                    .Build();

                CartItems.Remove(cartItem);
                CartItems.Add(newCartItem);
            }
            else
            {
                var newCartItem = new CartItem.Builder()
                    .WithProduct(product)
                    .WithQuantity(quantity)
                    .Build();

                CartItems.Add(newCartItem);
            }
        }

        public void RemoveItem(Product product)
        {
            var cartItem = CartItems
                .FirstOrDefault(i => i.Product.Id == product.Id);

            if (cartItem != null)
            {
                CartItems.Remove(cartItem);
            }
        }
    }
}

