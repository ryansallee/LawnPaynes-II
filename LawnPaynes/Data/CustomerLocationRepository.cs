using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawnPaynes.Models;
using System.Data.Entity;

namespace LawnPaynes.Data
{
    public class CustomerLocationRepository : BaseRepository<CustomerLocation>
    {
        public CustomerLocationRepository(LawnPaynesContext context)
        : base(context)
        {

        }
        public override CustomerLocation Get(int id, bool includeRelatedEntites = true)
        {
            var customerLocations = Context.CustomerLocations.AsQueryable();


            //if (includeRelatedEntites)
            //{
            //    customerLocations = customerLocations
            //        .Include(cl => cl.CustomerId);

            //}

            return customerLocations
                .Where(cl => cl.CustomerLocationId == id)
                .SingleOrDefault();

        }

        public override IList<CustomerLocation> GetList()
        {
            throw new NotImplementedException();
        }
    }
}