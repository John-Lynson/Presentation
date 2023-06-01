using System.Collections.Generic;

namespace Core.Models
{
    public class Cart
    {
        public string CartId { get; private set; }
        public List<CartItem> CartItems { get; private set; }

        private Cart(string cartId, List<CartItem> cartItems)
        {
            CartId = cartId;
            CartItems = cartItems;
        }

        public void SetCartItems(List<CartItem> cartItems)
        {
            CartItems = cartItems;
        }

        // Rest van de klasse...
    }
}