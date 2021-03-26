using FromFarmer.DataAccess.Context;
using FromFarmer.DataAccess.EntityFramework.FromFarmer.Repo;
using FromFarmer.Entities;
using System;

namespace FromFarmer.DataAccess.EntityFramework.FromFarmer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FromFarmerContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new FromFarmerContext();
        }

        public UnitOfWork(FromFarmerContext context)
        {
            _dbContext = context;
        }

        public IRepo<T> Repo<T>() where T : BaseEntity => new Repo<T>();

        public int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void CloseTransaction()
        {
            throw new NotImplementedException();
        }

        public void OpenTransaction()
        {
            throw new NotImplementedException();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
