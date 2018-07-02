using System.Data.Entity;
using LawnPaynes.Models;
using System;

namespace LawnPaynes.Data
{
    internal class DatabaseInitializer : CreateDatabaseIfNotExists<LawnPaynesContext>
    {
        protected override void Seed(LawnPaynesContext context)
        {
            var customer1 = new Customer()
            {
                Id = 1,
                Name = "John Doe",
                IsNew = true
            };

            context.Customers.Add(customer1);
            context.SaveChanges();

            var customer2 = new Customer()
            {
                Id = 2,
                Name = "Jane Doe",
                IsNew = false
            };

            context.Customers.Add(customer2);
            context.SaveChanges();
        
        }
    }
}