
using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class ProductsController : BaseApiController
  {
    private readonly IGenericRepository<Product> productRepo;
    private readonly IGenericRepository<ProductBrand> brandRepo;
    private readonly IGenericRepository<ProductType> typeRepo;
    private readonly IMapper mapper;

    public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo, IMapper mapper)
    {
      this.mapper = mapper;
      this.typeRepo = typeRepo;
      this.brandRepo = brandRepo;
      this.productRepo = productRepo;
    }

    // [Cached(6000)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
      var spec = new ProductsWithBrandsAndTypes(productParams);

      var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

      var totalItems = await productRepo.CountAsync(countSpec);

      var products = await productRepo.GetListAsync(spec);

      var data = mapper
          .Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

      return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    // [Cached(6000)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id) =>
        Ok(
            mapper.Map<Product, ProductDTO>(
                await productRepo.GetEntityWithSpec(
                    new ProductsWithBrandsAndTypes(
                        id
                    )
                )
            )
        );

    // [Cached(6000)]
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands() =>
        Ok(await brandRepo.GetAllAsync());

    // [Cached(6000)]
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() =>
        Ok(await typeRepo.GetAllAsync());
  }
}
