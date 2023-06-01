using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Order
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        // Private constructor for EF Core
        private Order()
        {
        }

        // Public constructor for creating orders
        public Order(int id, string userId, User user, DateTime orderDate, List<OrderItem> orderItems)
        {
            Id = id;
            UserId = userId;
            User = user;
            OrderDate = orderDate;
            OrderItems = orderItems;
        }
    }
}
