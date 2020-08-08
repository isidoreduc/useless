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
                  return await ctx.Products
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
            {
                  return await ctx.ProductBrands.ToListAsync();
            }

            public async Task<IEnumerable<Product>> GetProductsAsync()
            {
                  return await ctx.Products
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .ToListAsync();
            }

            public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
            {
                  return await ctx.ProductTypes.ToListAsync();
            }
      }
}