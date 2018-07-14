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

            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }
    }
}