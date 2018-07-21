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
        [HttpPost]
        public ActionResult Delete(int customerLocationId, int serviceId, int customerId)
        {
            var serviceCustomerLocation = Context.ServiceCustomerLocations
                .Where(scl => scl.CustomerLocationId == customerLocationId && scl.ServiceId == serviceId)
                .Include(scl => scl.Service)
                .Include(scl => scl.CustomerLocation)
                .SingleOrDefault();

            var serviceDeleted = serviceCustomerLocation.Service.ServiceName;
            var address = serviceCustomerLocation.CustomerLocation.Address;

            Context.ServiceCustomerLocations.Remove(serviceCustomerLocation);
            Context.SaveChanges();

            TempData["Message"] = serviceDeleted + " was removed from " + address + "!";
            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }

        public ActionResult Add(int customerLocationId)
        {
            var customerLocation = new GetCustomerLocationQuery(Context)
                .Execute((int)customerLocationId, false);

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ServiceCustomerLocationAddViewModel()
            {
                CustomerLocation = customerLocation,
                CustomerId = customerLocation.CustomerId          
            };

            viewModel.Initialize(Context);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ServiceCustomerLocationAddViewModel viewModel)
        {
            //Serverside Validation
            ServiceCustomerLocationValidator(viewModel);

            viewModel.CustomerLocation = new GetCustomerLocationQuery(Context)
                .Execute((int)viewModel.CustomerLocationId, false);

            if (ModelState.IsValid)
            {
                var serviceCustomerLocation = new ServiceCustomerLocation()
                {
                    CustomerLocationId = viewModel.CustomerLocationId,
                    ServiceId = viewModel.ServiceId
                };

                Context.ServiceCustomerLocations.Add(serviceCustomerLocation);
                Context.SaveChanges();

                var serviceAdded = Context.Services
                    .Where(s => s.ServiceId == serviceCustomerLocation.ServiceId)
                    .Select(s => s.ServiceName)
                    .SingleOrDefault();

                TempData["Message"] = serviceAdded + " was added to " + viewModel.CustomerLocation.Address + "!";
                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            viewModel.Initialize(Context);
            return View(viewModel);
        }

        private void ServiceCustomerLocationValidator(ServiceCustomerLocationAddViewModel viewModel)
        {
            if(ModelState.IsValidField("CustomerLocationId") && ModelState.IsValidField("ServiceId"))
            {
                if(Context.ServiceCustomerLocations
                    .Any(scl => scl.CustomerLocationId == viewModel.CustomerLocationId &&
                       scl.ServiceId == viewModel.ServiceId))
                {
                    ModelState.AddModelError("ServiceId", "This service already exists for this location!");
                }
            }

        }
    }
}