namespace FalconERP.Application.Printing;

public class ReceiptItem
{
    public string ProductName { get; set; } = "";

    public string UnitName { get; set; } = "";

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }
}