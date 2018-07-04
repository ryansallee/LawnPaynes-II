using LawnPaynes.Data;
using LawnPaynes.Models;
using LawnPaynes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

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

            public ActionResult Detail(int? id)

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var customer = _customerRepository.Get((int)id);

                
                if (customer == null)
                {
                    return HttpNotFound();
                }

                 return View(customer);
             }

            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var customer = _customerRepository.Get((int)id,
                    includeRelatedEntites: false);

                if (customer == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new CustomerEditViewModel()
                {
                    Customer = customer
                };

                return View(viewModel);

            }

            [HttpPost]
            public ActionResult Edit(CustomerEditViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    var customer = viewModel.Customer;

                    _customerRepository.Update(customer);

                    return RedirectToAction("Detail", new {id = customer.Id});
                }

            return View(viewModel);
            }
            

            [HttpPost]
            public ActionResult Delete(int id)
            {
                _customerRepository.Delete(id);

                return RedirectToAction("Index");
            }
    }
}