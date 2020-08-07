using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll() => Ok(ctx.Products);

        [HttpGet("{id}")]
        public IActionResult GetOne(int id) => Ok(ctx.Products.Find(id));        
    }
}
