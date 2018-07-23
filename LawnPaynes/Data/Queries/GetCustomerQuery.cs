using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawnPaynes.Models;
using System.Data.Entity;

namespace LawnPaynes.Data.Queries
{
    //This class contains an Execute Query to get a Customer. Since this query is used several times in the
    //Controllers, this class was made to better follow the DRY principle.
    public class GetCustomerQuery
    {
        private LawnPaynesContext _context = null;

        public GetCustomerQuery(LawnPaynesContext context)
        {
            _context = context;
        }

        //Method to execute a query to Load the Customer
        public Customer Execute(int id, bool includeRelatedEntities)
        {   
            //Load a Customer, CustomerLocations and the Services connected to that CustomerLocation if
            //includeRetlatedEntities is true.
            if(includeRelatedEntities)
            {
                return _context.Customers
                    .Include(c => c.CustomerLocations)
                    .Include("CustomerLocations.ServiceCustomerLocations.Service")
                    .Where(c => c.CustomerId == id)
                    .SingleOrDefault();
            }
            //Load only the customer.
            else
            {
                return _context.Customers
                    .Where(c => c.CustomerId == id)
                    .SingleOrDefault();
            }
        }
    }
}