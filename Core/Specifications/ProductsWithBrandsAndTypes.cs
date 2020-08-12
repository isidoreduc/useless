using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypes : BaseSpecification<Product>
    {
        // specification for getting all products with brands and types included
        public ProductsWithBrandsAndTypes(string sort, int? brandId, int? typeId) : base(x =>
            (!brandId.HasValue || x.ProductBrandId == brandId) &&
            (!typeId.HasValue || x.ProductTypeId == typeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);

            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        // specification for getting a product by id with brands and types included
        public ProductsWithBrandsAndTypes(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}