using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloomfiy_final.Models
{
    public class WishlistItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}