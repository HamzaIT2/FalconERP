namespace FalconERP.Application.DTOs;

public class SaleDetailsDto
{
    public int InvoiceNumber { get; set; }

    public string CustomerName { get; set; } = "";

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public List<SaleItemDetailsDto> Items { get; set; }
        = new();
}