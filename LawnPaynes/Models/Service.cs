using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    //The Service Model has many-to-many relationship with CustomerLocations via the ServiceCustomerLocation junction table.
    public class Service
    {
        public int ServiceId { get; set; }
        //Client side validation via unobtrusive jQuery validationby making sure the ServiceName is required and 
        //limiting its length to 200 characters.
        [Required, StringLength(100)]
        public string ServiceName { get; set; }

        //Each Service has a collection of CustomerLocations.
        public ICollection<ServiceCustomerLocation> ServiceCustomerLocations { get; } = new List<ServiceCustomerLocation>();
    }
}