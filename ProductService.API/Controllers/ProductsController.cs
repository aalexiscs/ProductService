using ProductService.Application.DTOs;
using ProductService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _productService.CreateProductAsync(productDto);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error while creating product");
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging product creation");
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if(product == null)
                {
                    return NotFound(new { message = "Producto no encontrado" });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product by ID");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.UpdateProductAsync(id, productDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", id);
                return NotFound(new { message = "Producto no encontrado" });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error while updating product");
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {ProductId}", id);
                return StatusCode(500, "An error occurred while updating the product.");
            }    
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
                return NotFound(new { message = "Producto no encontrado" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

    }
}
