using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace LawnPaynes.Data
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class
    {
        protected LawnPaynesContext Context { get; private set; }

        public BaseRepository(LawnPaynesContext context)
        {
            Context = context;
        }

        public abstract IList<TEntity> GetList();
        public abstract TEntity Get(int id, bool includeRelatedEntites = true);

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }
    }
}