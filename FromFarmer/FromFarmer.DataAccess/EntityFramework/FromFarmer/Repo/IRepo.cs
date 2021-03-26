using FromFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FromFarmer.DataAccess.EntityFramework.FromFarmer.Repo
{
    public interface IRepo<T> where T : BaseEntity
    {
        T Get(long id);
        List<T> GetAll();
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQueryable();
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
        void Delete(T entity);
    }
}
