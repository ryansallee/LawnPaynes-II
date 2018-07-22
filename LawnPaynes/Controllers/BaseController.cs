using System.Web.Mvc;
using LawnPaynes.Data;


namespace LawnPaynes.Controllers
{
    //The BaseController is inherited by the Customer, CustomerLocation, Service, ServiceCustomerLocation, and Home Controllers.
    //This controller has a property that instantiates the LawnPaynesContext in it's Constructor that allows this project to better adhere to the DRY principle.
    //Otherwise, each Controller would have multiple instances of using statements in each Controller. As well, I have overriden the normal
    //pattern of disposing the Context to prevent any possible memory leaks.
    
    public abstract class BaseController : Controller
    {
        private bool _disposed = false;
        
        protected LawnPaynesContext Context { get; private set; }
        
        //Set the Context property to the LawnPaynes Context.
        public BaseController()
        {
            Context = new LawnPaynesContext();
            
        }

        //Override the normal pattern of disposing the context.
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