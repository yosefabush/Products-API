using Microsoft.AspNetCore.Mvc;
using Products.Domain.Entities;
using Products.Models;
using Products.Services;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Product name is required");
                
            var products = await _service.GetByNameAsync(name);
            
            if (!products.Any())
                return NotFound();
                
            return Ok(products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductInsertDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Explicitly ignore the Id from the dto when creating a new Product
            var prod = new Product { Name = dto.Name, Price = dto.Price };
            var created = await _service.CreateAsync(prod);

            _logger.LogInformation("Product {ProductId} created at {Date}", created.Id, created.CreatedAt);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prod = new Product { Name = dto.Name, Price = dto.Price };
            var updated = await _service.UpdateAsync(id, prod);
            if (!updated)
                return NotFound();

            _logger.LogInformation("Product {ProductId} updated", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            _logger.LogInformation("Product {ProductId} deleted", id);
            return NoContent();
        }
    }
}
