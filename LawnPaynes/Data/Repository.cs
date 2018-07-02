using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawnPaynes.Data
{
    public class Repository
    {
        private LawnPaynesContext _context = null;

        public Repository(LawnPaynesContext context)
        {
            _context = context;
        }
    }
}