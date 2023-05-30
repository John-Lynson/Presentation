using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
