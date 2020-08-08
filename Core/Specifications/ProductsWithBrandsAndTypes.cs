using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypes : BaseSpecification<Product>
    {
        // specification for getting all products with brands and types included
        public ProductsWithBrandsAndTypes()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }

        // specification for getting a product by id with brands and types included
        public ProductsWithBrandsAndTypes(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}