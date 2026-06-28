using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class SaleItem : BaseEntity
{
    public int SaleId { get; set; }

    public Sale? Sale { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int ProductUnitId { get; set; }

    public ProductUnit? ProductUnit { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }
}