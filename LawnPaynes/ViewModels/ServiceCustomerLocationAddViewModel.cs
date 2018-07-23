using LawnPaynes.Data;
using LawnPaynes.Models;
using System.Linq;
using System.Web.Mvc;


namespace LawnPaynes.ViewModels
{
    public class ServiceCustomerLocationAddViewModel
    {
        //Bind the CustomerLocationId to the ViewModel
        public int CustomerLocationId
        {
            get { return CustomerLocation.CustomerLocationId; }
            set { CustomerLocation.CustomerLocationId = value; }
        }

        //Get and Set the CustomerID, CustomerLocation, ServiceID to the ViewModel.
        public CustomerLocation CustomerLocation { get; set; } = new CustomerLocation();
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }

        public SelectList ServiceSelectListItems { get; set; }

        //Method to Load a list of Services in a SelectList so that a user can select the service.
        public void Initialize(LawnPaynesContext context)
        {
            var serviceList = context.Services.ToList();
            ServiceSelectListItems = new SelectList(serviceList, "ServiceId", "ServiceName");
        }
    }
}