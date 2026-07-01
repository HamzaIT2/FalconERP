using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public decimal Quantity { get; set; }

    public decimal MinimumQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ProductUnit> Units { get; set; } = new List<ProductUnit>();
    public ICollection<PurchaseItem> PurchaseItems
    { get; set; } = new List<PurchaseItem>();
}