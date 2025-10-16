using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ProductService.Application.Services
{
    public class ProductApplicationService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductApplicationService> _logger;

        public ProductApplicationService(IProductRepository repository, ILogger<ProductApplicationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Guid> CreateProductAsync(CreateProductDto dto)
        {
            _logger.LogInformation("Creating a new product with name: {ProductName}", dto.Name);

            try
            {
                var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock);
                await _repository.AddAsync(product);

                _logger.LogInformation("Product created with ID: {ProductId}", product.Id);

                return product.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product with name: {ProductName}", dto.Name);
                throw;
            }
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);

            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found", id);
                return null;
            }

            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products");

            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);

            try
            {
                var product = await _repository.GetByIdAsync(id);

                if(product == null)
                {
                    _logger.LogWarning("Product with ID: {ProductId} not found", id);
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }
                
                product.Update(dto.Name, dto.Description, dto.Price, dto.Stock);
                await _repository.UpdateAsync(product);

                _logger.LogInformation("Product with ID: {ProductId} updated successfully", id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID: {ProductId}", id);
                throw;
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);

            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {ProductId} not found", id);
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                await _repository.DeleteAsync(id);

                _logger.LogInformation("Product with ID: {ProductId} deleted successfully", id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", id);
                throw;
            }
        }

        private ProductDto MapToDto(Product product) =>
            new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
    }
}
