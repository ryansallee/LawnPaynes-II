using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LawnPaynes.Data.Queries
{
    //This class contains an Execute Query to get a CustomerLocation. Since this query is used several times in the
    //Controllers, this class was made to better follow the DRY principle.
    public class GetCustomerLocationQuery
    {
        private LawnPaynesContext _context = null;

        public GetCustomerLocationQuery(LawnPaynesContext context)
        {
            _context = context;
        }

        //Method to execute a query to load a CustomerLocation.
        public CustomerLocation Execute(int id, bool includeCustomer)
        {
            //Load the CustomerLocation and Customer when includeCustomer is true.
            if(includeCustomer)
            {
                return _context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == id)
                .Include(cl => cl.Customer)
                .SingleOrDefault();
            }
            //Load only the CustomerLocation.
            else
            {
                return _context.CustomerLocations
                    .Where(cl => cl.CustomerLocationId == id)
                    .SingleOrDefault();
            }
        }
    }
}