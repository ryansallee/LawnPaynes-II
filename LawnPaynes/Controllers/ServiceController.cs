﻿using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
            Context.Services.Add(service);
            Context.SaveChanges();

            return RedirectToAction("Index");
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
            Context.Entry(service).State = EntityState.Modified;
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}