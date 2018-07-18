using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LawnPaynes.Controllers
{
    public class ServiceCustomerLocationController : BaseController
    {
        [HttpPost]
        public ActionResult Delete(int customerLocationId, int serviceId, int customerId)
        {
            var serviceCustomerLocation = Context.ServiceCustomerLocations.Find(customerLocationId, serviceId);
            Context.ServiceCustomerLocations.Remove(serviceCustomerLocation);
            Context.SaveChanges();

            var serviceDeleted = Context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => s.ServiceName)
                .SingleOrDefault();

            var address = Context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == customerLocationId)
                .Select(cl => cl.Address)
                .SingleOrDefault();

            TempData["Message"] = serviceDeleted + " was removed from " + address + "!";
            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }

        public ActionResult Add(int customerLocationId)
        {
            var customerLocation = Context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == customerLocationId)
                .SingleOrDefault();

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ServiceCustomerLocationAddViewModel()
            {
                CustomerLocation = customerLocation                
            };

            viewModel.Initialize(Context);
            viewModel.CustomerId = customerLocation.CustomerId;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ServiceCustomerLocationAddViewModel viewModel)
        {
            // Client-Side verification??
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

            var address = Context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == serviceCustomerLocation.CustomerLocationId)
                .Select(cl => cl.Address)
                .SingleOrDefault();

            TempData["Message"] = serviceAdded + " was added to " + address + "!";
            return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });

        }
    }
}