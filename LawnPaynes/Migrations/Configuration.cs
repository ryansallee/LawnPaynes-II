namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LawnPaynes.Data;
    using LawnPaynes.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<LawnPaynesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LawnPaynes.Data.LawnPaynesContext";
        }

        protected override void Seed(LawnPaynesContext context)
        {
            var customer1 = new Customer()
            {
                CustomerId = 1,
                Name = "John Doe",
                IsNew = true
            };

            context.Customers.AddOrUpdate(customer1);
            

            var customer2 = new Customer()
            {
                CustomerId = 2,
                Name = "Jane Doe",
                IsNew = false
            };

            context.Customers.AddOrUpdate(customer2);
        }
    }
}
