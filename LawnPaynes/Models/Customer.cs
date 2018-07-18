﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawnPaynes.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        public string Name { get; set; }
        [Display(Name ="New Customer")]
        public bool IsNew { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
        [EmailAddress(ErrorMessage = "The email provided is not a valid email address")]
        public string Email { get; set; }

        public ICollection<CustomerLocation> CustomerLocations { get; set; }
    }
}