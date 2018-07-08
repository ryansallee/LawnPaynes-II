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
    public class CustomerLocationController : BaseController
    {
        private CustomerLocationRepository _customerLocationRepository = null;
        private CustomerRepository _customerRepository = null;

        public CustomerLocationController()
        {
            _customerLocationRepository = new CustomerLocationRepository(Context);
            _customerRepository = new CustomerRepository(Context);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customerLocation = _customerLocationRepository.Get((int)id,
                includeRelatedEntites: true);

            if (customerLocation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerLocationEditViewModel()
            {
                CustomerLocation = customerLocation
            };



            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(CustomerLocationEditViewModel viewModel, int customerId)
        {
            if (ModelState.IsValid)
            {
                var customerLocation = viewModel.CustomerLocation;
                //Appears to be Hacky Code. Ask about.
                customerLocation.CustomerId = customerId;
                
                _customerLocationRepository.Update(customerLocation);

                return RedirectToAction("Detail", "Customer", new { id = customerLocation.CustomerId });
            }

            return View(viewModel);
        }

        public ActionResult Add(int customerId)
        {
            var customer = _customerRepository.Get(customerId);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerLocationAddViewModel();
            {
                viewModel.Customer = customer;
            };
            

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CustomerLocationAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customerLocation = new CustomerLocation()
                {
                    CustomerId = viewModel.CustomerId,
                    //Hacky code as well. Ask about.
                    Address = viewModel.CustomerLocation.Address
                };
                
                _customerLocationRepository.Add(customerLocation);

                return RedirectToAction("Detail", "Customer", new { id = viewModel.CustomerId });
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, int customerId)
        {
            _customerLocationRepository.Delete(id);

            return RedirectToAction("Detail", "Customer", new { id = customerId });
        }
    }
}