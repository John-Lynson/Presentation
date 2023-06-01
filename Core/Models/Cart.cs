using System.Collections.Generic;

namespace Core.Models
{
    public class Cart
    {
        public string CartId { get; private set; }
        public List<CartItem> CartItems { get; private set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}