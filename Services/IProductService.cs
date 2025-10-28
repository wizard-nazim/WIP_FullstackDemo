using InventoryApi.Dtos;

namespace InventoryApi.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync(string category = null);  // Filtering
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}