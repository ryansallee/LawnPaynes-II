using LawnPaynes.Data;
using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace LawnPaynes.Controllers
{
    public class CustomerController : BaseController
    {
  
        public ActionResult Index()
        {
            var customers = Context.Customers.
                ToList();

            return View(customers);
        }

        public ActionResult Add()
        {
            var viewModel = new CustomerAddViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CustomerAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = viewModel.Customer;
                
                Context.Customers.Add(customer);
                Context.SaveChanges();

                TempData["Message"] =  customer.Name + " was successfully added!";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

            public ActionResult Detail(int? id)

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var customer = Context.Customers
                .Include(c => c.CustomerLocations)
                .Include("CustomerLocations.ServiceCustomerLocations.Service")
                .Where(c => c.CustomerId == id)
                .SingleOrDefault();

                
                if (customer == null)
                {
                    return HttpNotFound();
                }

                customer.CustomerLocations = customer.CustomerLocations.ToList();

                 return View(customer);
             }

            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var customer = Context.Customers
                .Where(c => c.CustomerId == id)
                .SingleOrDefault();

                if (customer == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new CustomerEditViewModel()
                {
                    Customer = customer
                };
                
                

                return View(viewModel);

            }

            [HttpPost]
            public ActionResult Edit(CustomerEditViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    var customer = viewModel.Customer;

                    Context.Entry(customer).State = EntityState.Modified;
                    Context.SaveChanges();

                TempData["Message"] = customer.Name + " was successfully updated!";
                return RedirectToAction("Detail", new {id = customer.CustomerId});
                }

                return View(viewModel);
            }
            

            [HttpPost]
            public ActionResult Delete(int id)
            {
                var customer = Context.Customers.Find(id);
                Context.Customers.Remove(customer);
                Context.SaveChanges();

            TempData["Message"] = customer.Name + " was successfully deleted!";
            return RedirectToAction("Index");
            }
    }
}