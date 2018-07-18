﻿using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawnPaynes.ViewModels
{
    public class CustomerLocationEditViewModel : CustomerBaseViewModel
    {
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