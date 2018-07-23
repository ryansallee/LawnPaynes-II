using LawnPaynes.Models;

namespace LawnPaynes.ViewModels
{
    public class CustomerLocationAddViewModel: CustomerLocationBaseViewModel
    {
        //Binds the CustomerID to the ViewModel to allow the CustomerID to be set to the
        //CustomerLocation and allow the Controller to redirect the to the Customer Detail View
        //for the selected customer.
        public int CustomerId
        {
            get { return Customer.CustomerId;  }
            set { Customer.CustomerId = value; }
        }

      
    }
}