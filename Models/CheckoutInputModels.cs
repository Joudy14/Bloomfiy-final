using System.ComponentModel.DataAnnotations;

namespace Bloomfiy_final.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CheckoutInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }


        [Required]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits")]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiry must be MM/YY")]
        public string Expiry { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC must be 3 digits")]
        public string CVC { get; set; }
    }

}

