using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class ProductUnit : BaseEntity
{
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string UnitName { get; set; } = string.Empty;

    public decimal ConversionFactor { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal WholesalePrice { get; set; }

    public decimal RetailPrice { get; set; }

    public string? Barcode { get; set; }

    public bool IsDefault { get; set; }
}