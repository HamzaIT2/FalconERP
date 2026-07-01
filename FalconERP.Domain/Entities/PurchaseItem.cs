namespace FalconERP.Domain.Entities;

public class PurchaseItem
{
    public int Id { get; set; }

    public int PurchaseId { get; set; }

    public Purchase Purchase { get; set; } = null!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int ProductUnitId { get; set; }

    public ProductUnit ProductUnit { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }
}