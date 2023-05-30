using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    this.product = product;
                    return this;
                }

                public Builder WithQuantity(int quantity)
                {
                    this.quantity = quantity;
                    return this;
                }

                public CartItem Build()
                {
                    return new CartItem
                    {
                        Product = this.product,
                        Quantity = this.quantity
                    };
                }
            }
        }
    }
