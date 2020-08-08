using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
            Task<IEnumerable<Product>> GetProductsAsync();
            Task<Product> GetProductAsync(int id);
            Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
            Task<IEnumerable<ProductType>> GetProductTypesAsync();

      }
}