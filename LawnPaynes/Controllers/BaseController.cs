using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawnPaynes.Data;
using LawnPaynes.Models;
using System.Data.Entity;

namespace LawnPaynes.Controllers
{
    public abstract class BaseController : Controller
    {
        private bool _disposed = false;
        
        protected LawnPaynesContext Context { get; private set; }
        

        public BaseController()
        {
            Context = new LawnPaynesContext();
            
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}