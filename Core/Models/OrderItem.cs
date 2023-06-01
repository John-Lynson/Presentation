using System;

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

        public OrderItem(int id, int orderId, Order order, int productId, Product product, int quantity)
        {
            Id = id;
            OrderId = orderId;
            Order = order;
            ProductId = productId;
            Product = product;
            Quantity = quantity;
        }
    }
}
