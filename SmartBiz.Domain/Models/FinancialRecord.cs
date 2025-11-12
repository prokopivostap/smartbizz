using System.ComponentModel.DataAnnotations;

public class FinancialRecord
{
    [Key]
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int Income { get; set; }
    public string Type { get; set; }
    public string Records { get; set; }

    public string Currency { get; set; }
    public string Purpose { get; set; }
}