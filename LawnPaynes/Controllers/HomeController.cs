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
            //Only client side validation from the Customer Model is being checked to make sure the required fields have
            //data and that the data is in the appropriate format.
            if (ModelState.IsValid)
            {
                //The UpdateACustomer() method checks to see if an existing customer is making an update to their information.
                //If it is true, the Customer Entity is updated and a different TempData message is passed to the Contact View.
                if (UpdateACustomer(customer))
                {
                    TempData["Message"] = "Thank you for updating your information with us! We will get in touch with you shortly!";
                    return RedirectToAction("Contact");
                }
                else
                {
                    //Adds a customer if the UpdateAcustomer method returns false.
                    Context.Customers.Add(customer);
                    Context.SaveChanges();

                    TempData["Message"] = "Thank you for contacting us! We will get in touch with you shortly!";
                    return RedirectToAction("Contact");
                }
            }

            return View(customer);
        }

        //This method checks to see if any of the Customer information already exists by using Any() to see if a Customer Name
        // and Email or Customer Name and PhoneNumber exists in the customer Table. If so, the existing Customer Entity will be 
        // updated and the method returns return. If not, the method will return false to execute a Customer Add in the else clause.
        private bool UpdateACustomer(Customer customer)
        {
            //Check to see if a Customer Name and Email or Customer Name and PhoneNumber combination exists in the Customer Table.
            if (Context.Customers
                .Any(c => c.Name == customer.Name &&
                    c.Email == customer.Email) ||
                Context.Customers
                .Any(c => c.Name == customer.Name &&
                    c.PhoneNumber == customer.PhoneNumber))
            {
                //If a combination exists, Get the CustomerID of the Customer and set the ID of the customer variable so that the
                //customer can be updated and the method can return true.
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