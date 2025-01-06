using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found.");
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct([FromBody] ProductModel product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductModel product)
        {
            var existingProduct = _context.Products.SingleOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound("Product not found.");

            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found.");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}