using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypes : BaseSpecification<Product>
    {
        // specification for getting all products with brands and types included
        public ProductsWithBrandsAndTypes(ProductSpecParams prodParams) : base(x =>
            (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search)) &&
            (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) &&
            (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaging(prodParams.PageSize * (prodParams.PageIndex - 1), prodParams.PageSize);


            if(!string.IsNullOrEmpty(prodParams.Sort))
            {
                switch (prodParams.Sort)
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