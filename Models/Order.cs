using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloomfiy_final.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }  // Identity user
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }  // Pending, Preparing, Shipped, Completed

        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual List<OrderItem> Items { get; set; }
    }
}
