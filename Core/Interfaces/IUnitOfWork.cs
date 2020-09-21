using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    // implements IDisposable, so we can dispose of the context when finished
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}   