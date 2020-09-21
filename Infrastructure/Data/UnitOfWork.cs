using System;
using System.Collections;
using System.Security.Policy;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // if we haven't yet created a repository, hence a hashtable to store it, creates now said hashtable
            if(_repositories == null) 
                _repositories = new Hashtable();
            
            // checks what is the type of our repository (ex. Product, Order, Basket etc.)
            var type = typeof(TEntity).Name;

            // if the hashtable doesn't already contain a repository for our type
            if(!_repositories.ContainsKey(type))
            {
                // it creates a generic repository type
                var repositoryType = typeof(GenericRepository<>);
                // and then generates an instance of this repository using our specific type, and injects the unit of work context
                var repositoryInstance = 
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                // adds new repo to the hashtable
                _repositories.Add(type, repositoryInstance);
            }
            // returns the repository
            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}