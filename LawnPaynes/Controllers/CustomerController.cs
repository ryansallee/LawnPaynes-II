using LawnPaynes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawnPaynes.Controllers
{
    public class CustomerController : BaseController
    {
        private CustomerRepository _customerRepository = null;

        public CustomerController()
        {
            _customerRepository = new CustomerRepository(Context);
        }

        public ActionResult Index()
        {
            var customers = _customerRepository.GetList();

            return View(customers);
        }
    }
}