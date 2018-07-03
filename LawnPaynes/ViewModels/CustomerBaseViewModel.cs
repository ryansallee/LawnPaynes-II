using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawnPaynes.Data;

namespace LawnPaynes.ViewModels
{
    public abstract class CustomerBaseViewModel
    {
        public Customer Customer { get; set; } = new Customer();
    }
}