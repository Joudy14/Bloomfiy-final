using System.ComponentModel.DataAnnotations;

namespace Bloomfiy_final.Models
{
    public class CheckoutInputModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Payment (fake)
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        public string Expiry { get; set; }

        [Required]
        public string CVV { get; set; }
    }
}
