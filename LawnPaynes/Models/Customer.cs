using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    //The Customer Model has a one-to-many relationship with CustomerLocations.
    public class Customer
    {
        public int CustomerId { get; set; }

        //Data Annotations are used on following 5 properties to ensure that they are required input for any Customer, to control
        //string length, and that their data formats are enforced with client side validation via unaobtrusive validation.
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Display(Name ="New Customer")]
        public bool IsNew { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "The phone number provided is not a valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required, StringLength(2000, ErrorMessage="2000 characters is the maximum length.")]
        public string Comments { get; set; }
        [Required, EmailAddress(ErrorMessage = "The email provided is not a valid email address")]
        public string Email { get; set; }

        //Each Customer has a collection of their associated CustomerLocations.
        public ICollection<CustomerLocation> CustomerLocations { get; set; }
    }
}