using LawnPaynes.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LawnPaynes.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: Service
        public ActionResult Index()
        {
            var services = Context.Services
                .ToList();

            return View(services);
        }

        public ActionResult Add()
        {
            var service = new Service();

            return View(service);
        }

        [HttpPost]
        public ActionResult Add(Service service)
        {
            ServiceValidator(service);

            if (ModelState.IsValid)
            {
                Context.Services.Add(service);
                Context.SaveChanges();

                TempData["Message"] = "Service: " + service.ServiceName + " was successfully added!";
                return RedirectToAction("Index");
            }

            return View(service);
        }

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
            ServiceValidator(service);

            if (ModelState.IsValid)
            {
                Context.Entry(service).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "The update to the service was successful!";
                return RedirectToAction("Index");
            }

            return View(service);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = Context.Services.Find(id);
            Context.Services.Remove(service);
            Context.SaveChanges();

            TempData["Message"] = service.ServiceName + " was deleted!";
            return RedirectToAction("Index");
        }

        private void ServiceValidator(Service service)
        {
            if (ModelState.IsValidField("ServiceName") && ModelState.IsValidField("ServiceId"))
            {
                if (Context.Services
                    .Any(s => s.ServiceName == service.ServiceName))
                {
                    ModelState.AddModelError("ServiceId", service.ServiceName + " already exists as a service!");
                }
            }

        }
    }
}