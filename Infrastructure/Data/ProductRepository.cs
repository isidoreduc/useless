using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
      public class ProductRepository : IProductRepository
      {
            private readonly StoreContext ctx;

            public ProductRepository(StoreContext ctx)
            {
                this.ctx = ctx;
            }
            public async Task<Product> GetProductAsync(int id)
            {
                return await ctx.Products.FindAsync(id);
            }

            public async Task<IEnumerable<Product>> GetProductsAsync()
            {
                  return await ctx.Products.ToListAsync();
            }
      }
}