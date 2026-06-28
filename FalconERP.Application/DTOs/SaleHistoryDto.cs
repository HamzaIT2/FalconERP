namespace FalconERP.Application.DTOs;

public class SaleHistoryDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = "";

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }
}