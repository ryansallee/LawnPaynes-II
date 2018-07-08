using LawnPaynes.Data;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public ActionResult Contact(CustomerAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = viewModel.Customer;

                Context.Customers.Add(customer);

                return RedirectToAction("Contact");
            }

            return View(viewModel);
        }
    }
}