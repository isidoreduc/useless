using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
      public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
      {
            private readonly StoreContext ctx;
            public GenericRepository(StoreContext ctx)
            {
                this.ctx = ctx;

            }
            public async Task<T> GetOneAsync(int id)
            {
                return await ctx.Set<T>().FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                  return await ctx.Set<T>().ToListAsync();
            }

            public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
            {
                  return await ApplySpecification(spec).FirstOrDefaultAsync();
            }

            public async Task<IEnumerable<T>> GetListAsync(ISpecification<T> spec)
            {
                  return await ApplySpecification(spec).ToListAsync();
            }

            private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            {
                  return SpecificationEvaluator<T>.GetQuery(ctx.Set<T>().AsQueryable(), spec);
            }
      }
}