
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class ProductsController : ControllerBase
      {
            private readonly IGenericRepository<Product> productRepo;
            private readonly IGenericRepository<ProductBrand> brandRepo;
            private readonly IGenericRepository<ProductType> typeRepo;

            public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo)
            {
                  this.typeRepo = typeRepo;
                  this.brandRepo = brandRepo;
                  this.productRepo = productRepo;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Product>>> GetProducts() =>
                Ok(await productRepo.GetListAsync(new ProductsWithBrandsAndTypes()));

            [HttpGet("{id}")]
            public async Task<IActionResult> GetProductById(int id) =>
                Ok(await productRepo.GetEntityWithSpec(new ProductsWithBrandsAndTypes(id)));

            [HttpGet("brands")]
            public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands() =>
                Ok(await brandRepo.GetAllAsync());

            [HttpGet("types")]
            public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() =>
                Ok(await typeRepo.GetAllAsync());
      }
}
