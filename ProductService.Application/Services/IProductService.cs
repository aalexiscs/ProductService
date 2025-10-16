using ProductService.Application.DTOs;

namespace ProductService.Application.Services
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(CreateProductDto dto);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task DeleteProductAsync(Guid id);
    }
}