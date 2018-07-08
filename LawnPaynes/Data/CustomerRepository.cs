using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawnPaynes.Models;
using System.Data.Entity;

namespace LawnPaynes.Data
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(LawnPaynesContext context)
            :base(context)
        {

        }

        public override IList<Customer> GetList()
        {
            return Context.Customers
                .ToList();
        }

        public override Customer Get(int id, bool includeRelatedEntites = true)
        {
            var customers = Context.Customers.AsQueryable();
            

            if (includeRelatedEntites)
            {
                customers = customers
                    .Include(c => c.CustomerLocations);
                    
            }

            return customers
                .Where(c => c.CustomerId == id)
                .SingleOrDefault();

                        
        }
    }
}