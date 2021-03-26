using FromFarmer.DataAccess.EntityFramework.FromFarmer.Repo;
using FromFarmer.Entities;
using System;

namespace FromFarmer.DataAccess.EntityFramework.FromFarmer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepo<T> Repo<T>() where T : BaseEntity;
        int Save();
        void OpenTransaction();
        void CloseTransaction();
    }
}
