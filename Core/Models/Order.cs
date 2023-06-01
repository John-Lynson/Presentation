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

        private Order()
        {
            UserId = string.Empty;
            User = null!;
            OrderDate = DateTime.MinValue;
            OrderItems = new List<OrderItem>();
        }

        public Order(int id, string userId, DateTime orderDate) : this()
        {
            Id = id;
            UserId = userId;
            OrderDate = orderDate;
        }

        // Rest van je code...
    }
}