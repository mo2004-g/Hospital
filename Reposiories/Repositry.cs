using Hos.Date;
using Hos.Reposiories.IReposiories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hos.Reposiories
{
    public class Repositry<T> : IRepositry<T>   where T : class
    {
      //  ApplicationDbcontext dbcontext = new ApplicationDbcontext();
      private readonly ApplicationDbcontext dbcontext;
        public DbSet<T> dbset;

        public Repositry(ApplicationDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
            dbset =  dbcontext.Set<T>();
        }
        //CRUD

        public void Create(T entity) 
        {
            dbset.Add(entity);
        }
        public void Create(List<T> entities) 
        {
            dbset.AddRange(entities);
        }
        public void Edit(T entity) 
        {
            dbset.Update(entity);
        }
        public void Delete(T entity) 
        {
            dbset.Remove(entity);
        }
        public void Delete(List<T> entities)
        {
            dbset.RemoveRange(entities);
        }
        public void Commit()
        {
            dbcontext.SaveChanges();
        }
        public IEnumerable<T> Get(Expression<Func<T,bool>>? filter = null, Expression<Func<T, object>>[]? includes =null,bool tracked = true)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (includes != null)
            {
                foreach (var include in includes)
                {
                   query= query.Include(include);
                }
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return query.ToList();
        } 

        public T? Getone(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            return Get(filter, includes, tracked).FirstOrDefault();
        }
    }
}
