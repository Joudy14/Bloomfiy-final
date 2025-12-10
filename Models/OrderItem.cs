using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloomfiy_final.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
