using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawnPaynes.Models;
using System.Data.Entity;

namespace LawnPaynes.Data.Queries
{
    public class GetCustomerQuery
    {
        private LawnPaynesContext _context = null;

        public GetCustomerQuery(LawnPaynesContext context)
        {
            _context = context;
        }

        public Customer Execute(int id, bool includeRelatedEntities)
        {
            if(includeRelatedEntities)
            {
                return _context.Customers
                    .Include(c => c.CustomerLocations)
                    .Include("CustomerLocations.ServiceCustomerLocations.Service")
                    .Where(c => c.CustomerId == id)
                    .SingleOrDefault();
            }
            else
            {
                return _context.Customers
                    .Where(c => c.CustomerId == id)
                    .SingleOrDefault();
            }
        }
    }
}