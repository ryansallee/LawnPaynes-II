using LawnPaynes.Models;
using System.Linq;
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
            var customer = new Customer();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            CustomerValidator(customer);

            if (ModelState.IsValid)
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();

                TempData["Message"] = customer.Name + " was successfully added!";
                return RedirectToAction("Index");
            }

            return View(customer);
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


            return View(customer);

        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            Context.Entry(customer).State = EntityState.Modified;
            Context.SaveChanges();

            TempData["Message"] = customer.Name + " was successfully updated!";
            return RedirectToAction("Detail", new { id = customer.CustomerId });
            
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

        private void CustomerValidator(Customer customer)
        {
            if (ModelState.IsValidField("Name") && ModelState.IsValidField("Email") && ModelState.IsValidField("PhoneNumber"))
            {
                if (Context.Customers
                    .Any(c => c.Name == customer.Name &&
                        c.Email == customer.Email &&
                        c.PhoneNumber == customer.PhoneNumber))
                {
                    ModelState.AddModelError("Name", "This combination of Email, Phone Number, and Customer Name already exists!");
                }
            }

        }
    }
}