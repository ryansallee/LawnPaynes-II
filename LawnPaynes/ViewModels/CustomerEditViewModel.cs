using LawnPaynes.Data;
using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawnPaynes.ViewModels
{
	public class CustomerEditViewModel: CustomerBaseViewModel
	{

		public int Id
        {
            get { return Customer.Id; }
			set { Customer.Id = value; }
        }
	}
}