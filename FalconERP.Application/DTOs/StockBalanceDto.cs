namespace FalconERP.Application.DTOs;

public class StockBalanceDto
{
    public string ProductName { get; set; } = string.Empty;

    public decimal TotalIn { get; set; }

    public decimal TotalOut { get; set; }

    public decimal Balance { get; set; }
}