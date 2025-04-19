using Hos.Date;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace Hos.Reposiories.IReposiories
{
  public  interface  IRepositry<T> where T : class
    {

        public void Create(T entity);

        public void Create(List<T> entities);

        public void Edit(T entity);

        public void Delete(T entity);

        public void Delete(List<T> entities);

        public void Commit();
       
        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);

        public T? Getone(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);


    }
}
