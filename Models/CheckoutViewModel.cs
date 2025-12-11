using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Bloomfiy_final.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public decimal Total
        {
            get
            {
                if (CartItems == null) return 0;
                decimal sum = 0;
                foreach (var item in CartItems)
                    sum += item.TotalPrice;
                return sum;
            }
        }

        public CheckoutInputModel Input { get; set; }
    }
}
