using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Order() { }

        public class Builder
        {
            private int id;
            private string userId;
            private User user;
            private DateTime orderDate;
            private List<OrderItem> orderItems;

            public Builder WithId(int id)
            {
                this.id = id;
                return this;
            }

            public Builder WithUserId(string userId)
            {
                this.userId = userId;
                return this;
            }

            public Builder WithUser(User user)
            {
                this.user = user;
                return this;
            }

            public Builder WithOrderDate(DateTime orderDate)
            {
                this.orderDate = orderDate;
                return this;
            }

            public Builder WithOrderItems(List<OrderItem> orderItems)
            {
                this.orderItems = orderItems;
                return this;
            }

            public Order Build()
            {
                return new Order
                {
                    Id = this.id,
                    UserId = this.userId,
                    User = this.user,
                    OrderDate = this.orderDate,
                    OrderItems = this.orderItems
                };
            }
        }
    }
}