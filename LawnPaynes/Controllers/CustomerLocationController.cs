using LawnPaynes.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using LawnPaynes.Models;
using LawnPaynes.Data.Queries;

namespace LawnPaynes.Controllers
{
    public class CustomerLocationController : BaseController
    {
        //Gets the Edit View
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Query to Load the CustomerLocation
            var customerLocation = new GetCustomerLocationQuery(Context)
                .Execute(id: (int)id, includeCustomer: true);

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            //Set the CustomerLocation and Customer Properties of the CustomerLocationBaseViewModel
            //so that the needed Customer CustomerLocation properties will be displayed in the View.
            var viewModel = new CustomerLocationEditViewModel()
            {
                CustomerLocation = customerLocation,
                Customer = customerLocation.Customer
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerLocationEditViewModel viewModel)
        {
            var customerLocation = viewModel.CustomerLocation;
            //Server side Validation to ensure that the Address of the CustomerLocation is updated.
            CustomerLocationValidator(customerLocation);

            if (ModelState.IsValid)
            {
                Context.Entry(customerLocation).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "The address update was successful!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            //Reload the Customer to Display the name in the h2 of the Edit View if there is a ModelState Error
            // from not updating the address of the CustomerLocation.
            viewModel.Customer = new GetCustomerQuery(Context)
                                    .Execute((int)viewModel.CustomerId, false);
            return View(viewModel);
        }

        //Get the Add View.
        public ActionResult Add(int customerId)
        {
            //Load the Customer. 
            var customer = new GetCustomerQuery(Context)
                                    .Execute(id: (int)customerId, includeRelatedEntities: false);

            if (customer == null)
            {
                return HttpNotFound();
            }

            //Assign the customer variable to the Customer Property of the CustomerLocationBaseViewModel
            // to display the Customer Name in the h2 of the Add View.
            var viewModel = new CustomerLocationAddViewModel();
            {
                viewModel.Customer = customer;
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CustomerLocationAddViewModel viewModel)
        {
            var customerLocation = viewModel.CustomerLocation;
            customerLocation.CustomerId = viewModel.CustomerId;

            //Server side validation to make sure that the Address of the CustomerLocation doesn't exist
            //for this customer already. If so, throw a Model Error and return the Add Customer Location View.
            CustomerLocationValidator(customerLocation);

            if (ModelState.IsValid)
            {
                Context.CustomerLocations.Add(customerLocation);
                Context.SaveChanges();

                TempData["Message"] = "A new location at " + customerLocation.Address + " has been added!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            //Reload the customer if there is a ModelState Error to display the Customer Name.
            viewModel.Customer = new GetCustomerQuery(Context)
                                    .Execute((int)viewModel.CustomerId, false);

            return View(viewModel);
        }

        //Since this site is using a modal to delete CustomerLocations instead of a dedicated View,
        //we first Get the CustomerLocation using Find() with the ID associated to that CustomerLocation, 
        //and then remove the CustomerLocation.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int customerLocationId, int customerId)
        {
            var customerLocation = Context.CustomerLocations.Find(customerLocationId);
            Context.CustomerLocations.Remove(customerLocation);
            Context.SaveChanges();

            TempData["Message"] = "The Location at " + customerLocation.Address + " has been deleted!";
            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }

        //The CustomerLocationValidator method checks if there is a CustomerLocation that already exists in the CustomerLocation Table 
        //to prevent duplicate CustomerLocations from being added to the Table. As well, this server side validation ensures that an
        //address is being updated in the Edit CustomerLocation View.
        private void CustomerLocationValidator(CustomerLocation customerLocation)
        {
            //Check if there are any ModelState errors for the Address or CustomerId fields.
            if (ModelState.IsValidField("CustomerLocation.Address") && ModelState.IsValidField("CustomerLocation.CustomerId"))
            {
                //Check to see if there is a Combination of an Address and CustomerId in the CustomerLocation Table.
                if (Context.CustomerLocations
                    .Any(cl => cl.CustomerId == customerLocation.CustomerId &&
                       cl.Address == customerLocation.Address))
                {
                    //Add a ModelState Error to the Name field so that it can be displayed in the ValidationSummary
                    //in the Add CustomerLocation or Edit View.
                    ModelState.AddModelError("CustomerLocation.Address", "This address already exists for this customer," +
                        " or you have tried to edit an address and have not made any changes!");
                }
            }

        }
    }
}