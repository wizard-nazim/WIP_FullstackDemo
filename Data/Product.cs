namespace InventoryApi.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string Category { get; set; }  // For filtering
        // Hidden fields like CostPrice not in DTO
    }
}