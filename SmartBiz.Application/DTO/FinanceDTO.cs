namespace SmartBiz.Application.DTO
{
    public class FinanceDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Income { get; set; }
        public string Type { get; set; }
        public string Records { get; set; }
        public string Currency { get; set; }
        public string Purpose { get; set; }
        public decimal Sum { get; set; }
    }
}
