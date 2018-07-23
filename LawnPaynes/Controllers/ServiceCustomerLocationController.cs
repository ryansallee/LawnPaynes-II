using LawnPaynes.Data.Queries;
using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace LawnPaynes.Controllers
{
    public class ServiceCustomerLocationController : BaseController
    {
        //Since this site is using a modal to delete CustomerLocations instead of a dedicated View,
        //we first Get the CustomerLocation by its composite key of customerLocationId and ServiceID,
        //but will also egarly load its associated Service and CustomerLocation Entities.
        [HttpPost]
        public ActionResult Delete(int customerLocationId, int serviceId, int customerId)
        {
            var serviceCustomerLocation = Context.ServiceCustomerLocations
                .Where(scl => scl.CustomerLocationId == customerLocationId && scl.ServiceId == serviceId)
                .Include(scl => scl.Service)
                .Include(scl => scl.CustomerLocation)
                .SingleOrDefault();

            //Assign the associated ServiceName and Address to a variable so that it can be used
            //in the TempData message.
            var serviceDeleted = serviceCustomerLocation.Service.ServiceName;
            var address = serviceCustomerLocation.CustomerLocation.Address;

            Context.ServiceCustomerLocations.Remove(serviceCustomerLocation);
            Context.SaveChanges();

            TempData["Message"] = serviceDeleted + " was removed from " + address + "!";
            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }

        //Get the Add view
        public ActionResult Add(int customerLocationId)
        {
            //Load the CustomerLocation
            var customerLocation = new GetCustomerLocationQuery(Context)
                .Execute(id: (int)customerLocationId, includeCustomer: false);

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            //Set the CustomerLocation and CustomerId properties in the ViewModel
            var viewModel = new ServiceCustomerLocationAddViewModel()
            {
                CustomerLocation = customerLocation,
                CustomerId = customerLocation.CustomerId          
            };

            //Load the SelectList of Services.
            viewModel.Initialize(Context);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ServiceCustomerLocationAddViewModel viewModel)
        {
            //Server side validation to make sure the ServiceCustomerLocation does not already exist. If not,
            //a ModelState error will be thrown.
            ServiceCustomerLocationValidator(viewModel);

            //Load the Customer Location so that the CustomerLocation Address can be passed
            //to the TempData message or back to the ViewModel if a ModelState Error is thrown.
            viewModel.CustomerLocation = new GetCustomerLocationQuery(Context)
                .Execute(id: (int)viewModel.CustomerLocationId, includeCustomer: false);

            if (ModelState.IsValid)
            {
                //Set the ServiceId and CustomerLocationID in a new ServiceCustomerLocation
                var serviceCustomerLocation = new ServiceCustomerLocation()
                {
                    CustomerLocationId = viewModel.CustomerLocationId,
                    ServiceId = viewModel.ServiceId
                };

                Context.ServiceCustomerLocations.Add(serviceCustomerLocation);
                Context.SaveChanges();

                //Load the ServiceName using the ServiceId that was selected by the user so that it can be passed
                //to the TempData messsage.
                var serviceAdded = Context.Services
                    .Where(s => s.ServiceId == serviceCustomerLocation.ServiceId)
                    .Select(s => s.ServiceName)
                    .SingleOrDefault();

                TempData["Message"] = serviceAdded + " was added to " + viewModel.CustomerLocation.Address + "!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            //If there is a ModelState error, reload the SelectList of Services.
            viewModel.Initialize(Context);
            return View(viewModel);
        }
        //The ServiceCustomerLocationValidator method checks if there is a ServiceCustomerLocation that already exists in the  
        // ServiceustomerLocation Table to prevent duplicate ServiceCustomerLocations from being added to the Table. 
        private void ServiceCustomerLocationValidator(ServiceCustomerLocationAddViewModel viewModel)
        {
            //Check if there are any ModelState errors for the CustomerLocationID or ServiceId fields.
            if (ModelState.IsValidField("CustomerLocationId") && ModelState.IsValidField("ServiceId"))
            {
                //Check if there is a ServiceCustomerLocation with the same CustomerLocationId Service ID in the 
                //ServiceCustomerLocations Table.
                if(Context.ServiceCustomerLocations
                    .Any(scl => scl.CustomerLocationId == viewModel.CustomerLocationId &&
                       scl.ServiceId == viewModel.ServiceId))
                {
                    //Add a ModelState Error to the ServiceID field so that it can be displayed in the ValidationSummary
                    //in the Add ServiceCustomerLocation View.
                    ModelState.AddModelError("ServiceId", "This service already exists for this location!");
                }
            }

        }
    }
}