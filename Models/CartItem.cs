using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloomfiy_final.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public bool HasBouquet { get; set; }

        public decimal TotalPrice => (Price + (HasBouquet ? 10 : 0)) * Quantity;
    }
}
