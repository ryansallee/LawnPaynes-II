using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    public class ServiceCustomerLocation
    {
        [Key]
        [Column(Order = 0)]
        public int CustomerLocationId {get; set;}
        [Key]
        [Column(Order = 1)]
        public int ServiceId { get; set; }

    
    }
}