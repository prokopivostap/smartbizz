namespace SmartBiz.Application.DTO
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string StockStatus { get; set; }
    }
}
