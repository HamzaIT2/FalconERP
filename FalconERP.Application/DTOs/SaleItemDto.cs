namespace FalconERP.Application.DTOs;

public class SaleItemDto
{
    public string ProductName { get; set; } = string.Empty;

    public string UnitName { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public int ProductId { get; set; }

    public int ProductUnitId { get; set; }

    public decimal Total => Quantity * Price;
}