using LawnPaynes.Data;
using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LawnPaynes.ViewModels
{
    public class ServiceCustomerLocationAddViewModel
    {
        public int CustomerLocationId
        {
            get { return CustomerLocation.CustomerLocationId; }
            set { CustomerLocation.CustomerLocationId = value; }
        }

        public int CustomerId { get; set; }
        public CustomerLocation CustomerLocation { get; set; } = new CustomerLocation();
        public int ServiceId { get; set; }
        
        public SelectList ServiceSelectListItems { get; set; }

        public void Initialize(LawnPaynesContext context)
        {
            var serviceList = context.Services.ToList();
            ServiceSelectListItems = new SelectList(serviceList, "ServiceId", "ServiceName");
        }
    }
}