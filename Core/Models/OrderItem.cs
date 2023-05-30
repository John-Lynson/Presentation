using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public Order Order { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem() { }

        public class Builder
        {
            private int id;
            private int orderId;
            private Order order;
            private int productId;
            private Product product;
            private int quantity;

            public Builder WithId(int id)
            {
                this.id = id;
                return this;
            }

            public Builder WithOrderId(int orderId)
            {
                this.orderId = orderId;
                return this;
            }

            public Builder WithOrder(Order order)
            {
                this.order = order;
                return this;
            }

            public Builder WithProductId(int productId)
            {
                this.productId = productId;
                return this;
            }

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

            public OrderItem Build()
            {
                return new OrderItem
                {
                    Id = this.id,
                    OrderId = this.orderId,
                    Order = this.order,
                    ProductId = this.productId,
                    Product = this.product,
                    Quantity = this.quantity
                };
            }
        }
    }
}
