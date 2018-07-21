using LawnPaynes.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using LawnPaynes.Models;

namespace LawnPaynes.Controllers
{
    public class CustomerLocationController : BaseController
    {

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customerLocation = Context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == id)
                .Include(cl =>cl.Customer)
                .SingleOrDefault();

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerLocationEditViewModel()
            {
                CustomerLocation = customerLocation,
                Customer = customerLocation.Customer
            };

             return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(CustomerLocationEditViewModel viewModel)
        {
            var customerLocation = viewModel.CustomerLocation;
            CustomerLocationValidator(customerLocation);

            if (ModelState.IsValid)
            {                              
                Context.Entry(customerLocation).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "The address update was successful!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            viewModel.Customer = Context.Customers
                .Where(c => c.CustomerId == viewModel.CustomerId)
                .SingleOrDefault();
            return View(viewModel);
        }

        public ActionResult Add(int customerId)
        {
            var customer = Context.Customers
                .Where(c => c.CustomerId == customerId)
                .SingleOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerLocationAddViewModel();
            {
                viewModel.Customer = customer;
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CustomerLocationAddViewModel viewModel)
        {
            var customerLocation = viewModel.CustomerLocation;
            customerLocation.CustomerId = viewModel.CustomerId;

            CustomerLocationValidator(customerLocation);

            if (ModelState.IsValid)
            {
               Context.CustomerLocations.Add(customerLocation);
                Context.SaveChanges();

                TempData["Message"] = "A new location at " + customerLocation.Address + " has been added!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            viewModel.Customer = Context.Customers
                .Where(c => c.CustomerId == viewModel.CustomerId)
                .SingleOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int customerLocationId, int customerId)
        {
            var customerLocation = Context.CustomerLocations.Find(customerLocationId);
            Context.CustomerLocations.Remove(customerLocation);
            Context.SaveChanges();

            TempData["Message"] = "The Location at " + customerLocation.Address + " has been deleted!";
            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }

        private void CustomerLocationValidator(CustomerLocation customerLocation)
        {
            if (ModelState.IsValidField("CustomerLocation.Address") && ModelState.IsValidField("CustomerLocation.CustomerId"))
            {
                if (Context.CustomerLocations
                    .Any(cl => cl.CustomerId == customerLocation.CustomerId &&
                       cl.Address == customerLocation.Address))
                {
                    ModelState.AddModelError("CustomerLocation.Address", "This address already exists for this customer," +
                        " or you have tried to edit an address and have not made any changes!");
                }
            }

        }
    }
}