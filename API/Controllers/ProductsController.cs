
using API.DTOs;
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

            [HttpGet]
            public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string sort) =>
                Ok(
                    mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(
                        await productRepo.GetListAsync(
                            new ProductsWithBrandsAndTypes(sort)
                        )
                    )
                );

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

            [HttpGet("brands")]
            public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands() =>
                Ok(await brandRepo.GetAllAsync());

            [HttpGet("types")]
            public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() =>
                Ok(await typeRepo.GetAllAsync());
      }
}
