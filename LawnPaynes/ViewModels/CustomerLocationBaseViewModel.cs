using LawnPaynes.Models;

namespace LawnPaynes.ViewModels
{
    //This a base ViewModel for the CustomerLocatiom Add and Edit. The Customer and CustomerLocation properties
    //are used in both models and helps this site adhere to the DRY principle.
    public abstract class CustomerLocationBaseViewModel
    {
        public Customer Customer { get; set; } = new Customer();
        public CustomerLocation CustomerLocation { get; set; } = new CustomerLocation();
    }
}