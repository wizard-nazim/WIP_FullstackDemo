using AutoMapper;
using InventoryApi.Data;
using InventoryApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, IMapper mapper, ILogger<ProductService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllAsync(string category = null)
        {
            // LINQ Queries and Async (Fundamental 2)
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);  // Filtering
            var products = await query.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);  // DTO mapping
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            // Business Logic: e.g., Check if stock is positive
            if (dto.StockQty < 0) throw new Exception("Stock cannot be negative");
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product created: {Name}", product.Name);  // Logging
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");
            if (dto.StockQty.HasValue && dto.StockQty < 0) throw new Exception("Stock cannot be negative");
            _mapper.Map(dto, product);  // Only updates provided fields
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product updated: {Name}", product.Name);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}