namespace Core.Models
{
    public class CartItem
    {
        public string CartId { get; private set; }
        public Product Product { get; private set; }

        public void SetCartId(string cartId)
        {
            CartId = cartId;
        }

        public void SetProduct(Product product)
        {
            Product = product;
        }

        // Rest van de klasse...
    }
}
