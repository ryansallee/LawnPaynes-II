using LawnPaynes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LawnPaynes.Data.Queries
{
    public class GetCustomerLocationQuery
    {
        private LawnPaynesContext _context = null;

        public GetCustomerLocationQuery(LawnPaynesContext context)
        {
            _context = context;
        }

        public CustomerLocation Execute(int id, bool includeCustomer)
        {
            if(includeCustomer)
            {
                return _context.CustomerLocations
                .Where(cl => cl.CustomerLocationId == id)
                .Include(cl => cl.Customer)
                .SingleOrDefault();
            }
            else
            {
                return _context.CustomerLocations
                    .Where(cl => cl.CustomerLocationId == id)
                    .SingleOrDefault();
            }
        }
    }
}