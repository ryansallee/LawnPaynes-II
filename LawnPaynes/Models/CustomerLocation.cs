using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawnPaynes.Models;

namespace LawnPaynes.Models
{
    //The CustomerLocation Model has a one-to-many relationship with Customers, and a many-to-many relationship with
    //services via the ServiceCustomerLocations junction table.
    public class CustomerLocation
    {
        public int CustomerLocationId { get; set; }

        //Client side validation via unobtrusive jQuery validaation by making sure the Address is required 
        //and limiting its length to 200 characters.
        [Required, StringLength(200)]
        public string Address { get; set; }

        //Each CustomerLocation is associated with a Customer.
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        //Each CustomerLocation has a collection of ServiceCustomerLocations so that Services can be loaded with a CustomerLocation
        public ICollection<ServiceCustomerLocation> ServiceCustomerLocations { get; set; }
    }
}