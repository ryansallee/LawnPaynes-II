using LawnPaynes.Data;
using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawnPaynes.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Our Work
        public ActionResult OurWork()
        {
            return View();
        }

        // GET: Services
        public ActionResult Services()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            var customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Contact(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (UpdateACustomer(customer))
                {
                    TempData["Message"] = "Thank you for updating your information with us! We will get in touch with you shortly!";
                    return RedirectToAction("Contact");
                }
                else
                {
                    Context.Customers.Add(customer);
                    Context.SaveChanges();

                    TempData["Message"] = "Thank you for contacting us! We will get in touch with you shortly!";
                    return RedirectToAction("Contact");
                }
            }

            return View(customer);
        }

        private bool UpdateACustomer(Customer customer)
        {            
                if (Context.Customers
                    .Any(c => c.Name == customer.Name &&
                        c.Email == customer.Email) ||
                    Context.Customers
                    .Any(c => c.Name == customer.Name &&
                        c.PhoneNumber == customer.PhoneNumber))
                {
                var customerIdUpdate = Context.Customers
                        .Where(c => c.Name == customer.Name &&
                               c.Email == customer.Email ||
                               c.Name == customer.Name &&
                               c.PhoneNumber == customer.PhoneNumber)
                        .Select(c => c.CustomerId)
                        .SingleOrDefault();

                    customer.CustomerId = customerIdUpdate;

                    Context.Entry(customer).State = EntityState.Modified;
                    Context.SaveChanges();

                    return true;
                 }
            return false;
            
        }

    }
}