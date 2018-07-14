using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LawnPaynes.Controllers
{
    public class ServiceCustomerLocationController : BaseController
    {
        [HttpPost]
        public ActionResult Delete(int customerLocationId, int serviceId)
        {
            var serviceCustomerLocation = Context.ServiceCustomerLocations.Find(customerLocationId, serviceId);

            Context.ServiceCustomerLocations.Remove(serviceCustomerLocation);

            return RedirectToAction("Home","Index");
        }
    }
}