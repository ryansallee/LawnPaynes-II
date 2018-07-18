using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LawnPaynes.Models;

namespace LawnPaynes.Models
{
    public class CustomerLocation
    {
        public int CustomerLocationId { get; set; }
        [Required]
        public string Address { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<ServiceCustomerLocation> ServiceCustomerLocations { get; set; }
    }
}