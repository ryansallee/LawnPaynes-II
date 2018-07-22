using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Display(Name ="New Customer")]
        public bool IsNew { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$",ErrorMessage = "The phone number provided is not a valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required, StringLength(2000, ErrorMessage="2000 characters is the maximum length.")]
        public string Comments { get; set; }
        [Required, EmailAddress(ErrorMessage = "The email provided is not a valid email address")]
        public string Email { get; set; }

        public ICollection<CustomerLocation> CustomerLocations { get; set; }
    }
}