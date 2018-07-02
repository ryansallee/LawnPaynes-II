using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawnPaynes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Our Work
        public ActionResult OurWork()
        {
            return View();
        }

        // GET: Services
        public ActionResult Services()
        {
            return View();
        }
    }
}