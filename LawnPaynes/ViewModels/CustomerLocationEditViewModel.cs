using LawnPaynes.Models;

namespace LawnPaynes.ViewModels
{
    public class CustomerLocationEditViewModel : CustomerLocationBaseViewModel
    {
        //Bind the CustomerLocationId and CustomerID properties of the CustomerLocation to the viewModel.
        public int Id
        {
            get { return CustomerLocation.CustomerLocationId; }
            set { CustomerLocation.CustomerLocationId = value; }
        }

        public int CustomerId 
            {
                get {return CustomerLocation.CustomerId;}
                set {CustomerLocation.CustomerId = value;}
            }

        
    }
}