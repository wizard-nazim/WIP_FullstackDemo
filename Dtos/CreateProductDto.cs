using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Dtos
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQty { get; set; }

        public string Category { get; set; }
    }
}