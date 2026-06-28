using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class InventoryTransaction : BaseEntity
{
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int ProductUnitId { get; set; }

    public ProductUnit? ProductUnit { get; set; }

    public decimal Quantity { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; }

    public string? Notes { get; set; }
}