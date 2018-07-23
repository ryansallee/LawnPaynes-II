using LawnPaynes.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LawnPaynes.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: Service Index
        public ActionResult Index()
        {
            var services = Context.Services
                .ToList();

            return View(services);
        }

        //Get: Service Add  
        public ActionResult Add()
        {
            var service = new Service();

            return View(service);
        }

        [HttpPost]
        public ActionResult Add(Service service)
        {
            //Server side validation to ensure that the Service does not already exist. If so, a ModelState error
            // will be thrown.
            ServiceValidator(service);

            //As well, client side validation is also done to make sure there is no blank serices.
            if (ModelState.IsValid)
            {
                Context.Services.Add(service);
                Context.SaveChanges();

                TempData["Message"] = "Service: " + service.ServiceName + " was successfully added!";
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //Get: Service Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = Context.Services
                .Where(s => s.ServiceId == id)
                .SingleOrDefault();

            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service);
        }

        [HttpPost]
        public ActionResult Edit(Service service)
        {
            //Server side validation to ensure that the Service does not already exist. If so, a ModelState error
            // will be thrown.
            ServiceValidator(service);

            //As well, client side validation is also done to make sure there is no blank serices.
            if (ModelState.IsValid)
            {
                Context.Entry(service).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "The update to the service was successful!";
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //Since this site is using a modal to delete Servvices instead of a dedicated View,
        //we first Get the customer using Find() with the ID associated to that customer, and then remove the customer.
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = Context.Services.Find(id);
            Context.Services.Remove(service);
            Context.SaveChanges();

            TempData["Message"] = service.ServiceName + " was deleted!";
            return RedirectToAction("Index");
        }

        //The ServiceValidator method checks if there is a Service that already exists in the Customer Table to prevent multiple
        //Services from being added to the Table.
        private void ServiceValidator(Service service)
        {
            //Check if there are any ModelState errors for the ServiceName or Service Id fields
            if (ModelState.IsValidField("ServiceName") && ModelState.IsValidField("ServiceId"))
            {

                //Check to see if the ServiceName already exists in the Service Table.
                if (Context.Services
                    .Any(s => s.ServiceName == service.ServiceName))
                {
                    //Add a ModelState Error to the ServiceName field so that it can be displayed in the ValidationSummary
                    //in the Add or Edit Service View.
                    ModelState.AddModelError("ServiceName", service.ServiceName + " already exists as a service!");
                }
            }

        }
    }
}