using FromFarmer.DataAccess.Context;
using FromFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FromFarmer.DataAccess.EntityFramework.FromFarmer.Repo
{
    public class Repo<T> : IRepo<T> where T : BaseEntity
    {
        private FromFarmerContext _ctx;

        public Repo()
        {
            _ctx = new FromFarmerContext();
        }

        public T Get(long id)
        {
            using (FromFarmerContext context = new FromFarmerContext())
            {
                DbSet<T> _dbSet = context.Set<T>();

                return _dbSet.AsNoTracking().FirstOrDefault(x => x.IS_DELETED == 0 && x.ID == id);
            }
        }

        public List<T> GetAll()
        {
            using (FromFarmerContext context = new FromFarmerContext())
            {
                DbSet<T> _dbSet = context.Set<T>();

                return _dbSet.AsNoTracking().Where(x => x.IS_DELETED == 0).ToList();
            }
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> pregs = null)
        {
            DbSet<T> _dbSet = _ctx.Set<T>();

            if (pregs != null)
                return _dbSet.AsNoTracking().Where(pregs).Where(x => x.IS_DELETED == 0);
            else
                return _dbSet.AsNoTracking().Where(x => x.IS_DELETED == 0);
        }

        public IQueryable<T> GetAllQueryable()
        {
            DbSet<T> _dbSet = _ctx.Set<T>();

            return _dbSet.AsNoTracking().Where(x => x.IS_DELETED == 0);
        }

        public T Add(T entity)
        {
            using (FromFarmerContext context = new FromFarmerContext())
            {
                entity.CREATE_AT = DateTime.Now;
                entity.MODIFIED_AT = DateTime.Now;
                entity.IS_DELETED = 0;
                entity.IS_MODIFIED = 0;

                var a = context.Add(entity);

                context.SaveChanges();

                return entity;
            }
        }

        public T Update(T entity)
        {
            using (FromFarmerContext context = new FromFarmerContext())
            {
                entity.MODIFIED_AT = DateTime.Now;
                entity.IS_MODIFIED = 1;

                context.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;

                context.SaveChanges();

                return entity;
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                entity.IS_DELETED = 1;
                entity.MODIFIED_AT = DateTime.Now;

                Update(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = Get(id);

            Delete(entity);
        }
    }
}
