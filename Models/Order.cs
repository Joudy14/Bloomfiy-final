using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bloomfiy_final.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }  // Pending, Processing, Delivered...

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
