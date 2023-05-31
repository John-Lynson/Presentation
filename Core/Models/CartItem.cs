
namespace Core.Models
{
    public class CartItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        private CartItem() { }

        public class Builder
        {
            private Product product;
            private int quantity;

            public Builder WithProduct(Product product)
            {
                this.product = product ?? throw new ArgumentNullException(nameof(product));
                return this;
            }

            public Builder WithQuantity(int quantity)
            {
                this.quantity = quantity;
                return this;
            }

            public CartItem Build()
            {
                if (product == null)
                {
                    throw new InvalidOperationException("Product cannot be null");
                }

                return new CartItem
                {
                    Product = this.product,
                    Quantity = this.quantity
                };
            }
        }
    }
}

