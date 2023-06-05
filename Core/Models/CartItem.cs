namespace Core.Models
{
    public class CartItem
    {
        public string CartId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }  // Added Quantity property

        public void SetCartId(string cartId)
        {
            CartId = cartId;
        }

        public void SetProduct(Product product)
        {
            Product = product;
        }

        public void SetQuantity(int quantity)  // Added method to set Quantity
        {
            Quantity = quantity;
        }

        // Rest van de klasse...
    }
}

