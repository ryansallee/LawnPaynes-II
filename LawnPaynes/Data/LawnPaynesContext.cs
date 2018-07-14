using System;
using System.Data.Entity;
using LawnPaynes.Models;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace LawnPaynes.Data
{

    public class LawnPaynesContext : DbContext
    {
        // Your context has been configured to use a 'Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LawnPaynes.Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Context' 
        // connection string in the application configuration file.
        public LawnPaynesContext()
            : base("name=LawnPaynesContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerLocation> CustomerLocations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCustomerLocation> ServiceCustomerLocations { get; set; }



        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}