
using Core.Entities;
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
    public class ProductController : ControllerBase
    {
        private readonly StoreContext ctx;

        public ProductController(StoreContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll() =>  Ok(await ctx.Products.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id) => 
            Ok(await ctx.Products.FirstOrDefaultAsync(p => p.Id == id));        
    }
}
