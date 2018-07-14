using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public ICollection<ServiceCustomerLocation> CustomerLocations { get; } = new List<ServiceCustomerLocation>();
    }
}