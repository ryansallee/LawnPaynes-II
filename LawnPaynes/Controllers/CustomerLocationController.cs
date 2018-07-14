using LawnPaynes.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

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
                .SingleOrDefault();

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerLocationEditViewModel()
            {
                CustomerLocation = customerLocation
            };

             return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(CustomerLocationEditViewModel viewModel/*, int customerId*/)
        {
            if (ModelState.IsValid)
            {
                var customerLocation = viewModel.CustomerLocation;
                ////Appears to be Hacky Code. Ask about.
                //customerLocation.CustomerId = customerId;

                Context.Entry(customerLocation).State = EntityState.Modified;
                Context.SaveChanges();

                return RedirectToAction("Detail", "Customer", new { id = customerLocation.CustomerId });
            }

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
            if (ModelState.IsValid)
            {
                var customerLocation = viewModel.CustomerLocation;
                customerLocation.CustomerId = viewModel.CustomerId;
                
                Context.CustomerLocations.Add(customerLocation);
                Context.SaveChanges();

                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, int customerId)
        {
            var customerLocation = Context.CustomerLocations.Find(id);
            Context.CustomerLocations.Remove(customerLocation);
            Context.SaveChanges();

            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }
    }
}