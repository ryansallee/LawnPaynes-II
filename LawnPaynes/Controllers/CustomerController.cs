using LawnPaynes.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using LawnPaynes.Data.Queries;

namespace LawnPaynes.Controllers
{
    public class CustomerController : BaseController
    {
        //Return a list of Customers for the Index Table of Customers.
        public ActionResult Index()
        {
            var customers = Context.Customers.
                ToList();

            return View(customers);
        }

        //Get the Add Customer View.
        public ActionResult Add()
        {
            var customer = new Customer();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            //Server side validation to ensure that duplicate customers are not added by throwing a model state error.
            CustomerValidator(customer);

            //Client side validation is also checked to make sure that the fields are of valid input types. If the ModelState is 
            //not valid then the view with the errors displayed to the user.
            if (ModelState.IsValid)
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();

                TempData["Message"] = customer.Name + " was successfully added!";
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //Get the Customer Detail View.
        public ActionResult Detail(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //This query eagerly loads the CustomerLocations as well the services
            // as the Services do not have a direct relationship with Customers.
            var customer = new GetCustomerQuery(Context)
                .Execute(id: (int)id, includeRelatedEntities: true);


            if (customer == null)
            {
                return HttpNotFound();
            }

            //List the CustomerLocations related to the Customer so that a foreach loop can iterate through them.
            customer.CustomerLocations = customer.CustomerLocations.ToList();

            return View(customer);
        }

        //Get the Edit View.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get the Customer and the Customer only.
            var customer = new GetCustomerQuery(Context)
                .Execute(id: (int)id, includeRelatedEntities: false);

            if (customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            //Check if the fields have valid input formats and required fields have data. If not,
            // throw a model state error and return the edit view.
            if (ModelState.IsValid)
            {
                Context.Entry(customer).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = customer.Name + " was successfully updated!";
                return RedirectToAction("Detail", new { id = customer.CustomerId });
            }

            return View(customer);
        }

        //Since this site is using a modal to delete Customers instead of a dedicated View,
        //we first Get the customer using Find() with the ID associated to that customer, and then remove the customer.
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var customer = Context.Customers.Find(id);
            Context.Customers.Remove(customer);
            Context.SaveChanges();

            TempData["Message"] = customer.Name + " was successfully deleted!";
            return RedirectToAction("Index");
        }

        //The CustomerValidator method checks if there is a Customer that already exists in the Customer Table to prevent multiple
        //customers from being added to the Table.
        private void CustomerValidator(Customer customer)
        {
            //Check if there are any ModelState errors for the Name, Email, or Phone Number fields.
            if (ModelState.IsValidField("Name") && ModelState.IsValidField("Email") && ModelState.IsValidField("PhoneNumber"))
            {
                //Check to see if there is a Combination of Customer, Name, and Email in the Customer Table
                if (Context.Customers
                    .Any(c => c.Name == customer.Name &&
                        c.Email == customer.Email &&
                        c.PhoneNumber == customer.PhoneNumber))
                {
                    //Add a ModelState Error to the Name field so that it can be displayed in the ValidationSummary
                    //in the Add Customer View.
                    ModelState.AddModelError("Name", "This combination of Email, Phone Number, and Customer Name already exists!");
                }
            }

        }
    }
}