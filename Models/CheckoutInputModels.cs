using System.ComponentModel.DataAnnotations;

namespace Bloomfiy_final.Models
{
    public class CheckoutInputModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string Phone { get; set; }

        // Fake credit card fields
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string CVV { get; set; }
    }
}

