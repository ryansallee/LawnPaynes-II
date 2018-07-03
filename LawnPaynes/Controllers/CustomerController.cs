using LawnPaynes.Data;
using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        public ActionResult Add()
        {
            var viewModel = new CustomerAddViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CustomerAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = viewModel.Customer;

                _customerRepository.Add(customer);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}