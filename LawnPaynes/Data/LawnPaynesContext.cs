using System;
using System.Data.Entity;
using LawnPaynes.Models;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace LawnPaynes.Data
{

    public class LawnPaynesContext : DbContext
    {
        public LawnPaynesContext()
            : base("name=LawnPaynes")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerLocation> CustomerLocations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCustomerLocation> ServiceCustomerLocations { get; set; }
    }


}