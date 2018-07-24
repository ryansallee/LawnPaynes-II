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
                IsNew = false,
                PhoneNumber = "(345) 678-1234",
                Email = "john@google.com",
                Comments = "Test a comment!"
            };


            var customer2 = new Customer()
            {
                CustomerId = 2,
                Name = "Jane Doe",
                IsNew = true,
                PhoneNumber = "(800) 123-4567",
                Email = "jane@google.com",
                Comments = "Test a comment again!"
            };

            var customer3 = new Customer()
            {
                CustomerId = 3,
                Name = "Ryan Sallee",
                IsNew = false,
                PhoneNumber = "(502) 111-2222",
                Email = "ryan@google.com",
                Comments = "Visit weekly."
            };
            context.Customers.AddOrUpdate(customer1, customer2, customer3);

            var customerLocation1 = new CustomerLocation()
            {
                CustomerLocationId = 1,
                Address = "1333 Jones St, Louisville, KY 40218",
                CustomerId = 1
            };

            var customerLocation2 = new CustomerLocation()
            {
                CustomerLocationId = 2,
                Address = "5200 Indian Trl, Louisville, KY 40218",
                CustomerId = 3
            };

            context.CustomerLocations.AddOrUpdate(customerLocation1, customerLocation2);

            var service1 = new Service()
            {
                ServiceId = 1,
                ServiceName = "Cut"
            };

            var service2 = new Service()
            {
                ServiceId = 2,
                ServiceName = "Trim"
            };

            var service3 = new Service()
            {
                ServiceId = 3,
                ServiceName = "Landscaping"
            };

            context.Services.AddOrUpdate(service1, service2, service3);

            var serviceCustomerLocation1 = new ServiceCustomerLocation()
            {
                CustomerLocationId = 2,
                ServiceId = 1
            };

            var serviceCustomerLocation2 = new ServiceCustomerLocation()
            {
                CustomerLocationId = 2,
                ServiceId = 2
            };

            var serviceCustomerLocation3 = new ServiceCustomerLocation()
            {
                CustomerLocationId = 1,
                ServiceId = 2
            };

            context.ServiceCustomerLocations.AddOrUpdate(serviceCustomerLocation1, serviceCustomerLocation2, serviceCustomerLocation3);

        }
    }
}
