using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    //ServiceCustomerLocation is the junction table for the many-to-many relationship between Services and CustomerLocations
    public class ServiceCustomerLocation
    {
        //Composite Key of CustomerLocationId and ServiceID
        [Key, Column(Order =0)]
        public int CustomerLocationId { get; set; }
        [Key, Column(Order =1)]
        public int ServiceId { get; set; }

        //Each ServiceCustomerLocation has a CustomerLocation and Service associated with it that can be loaded.
        public CustomerLocation CustomerLocation { get; set; }
        public Service Service { get; set; }
    }
}