using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawnPaynes.ViewModels
{
    public class CustomerLocationAddViewModel: CustomerBaseViewModel
    {

        public int CustomerId
        {
            get { return Customer.CustomerId;  }
            set { Customer.CustomerId = value; }
        }

        //public Customer Customer { get; set; } = new Customer();
        

    }
}