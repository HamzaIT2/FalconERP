namespace FalconERP.Domain.Entities;

public class Purchase
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = "";
    public DateTime PurchaseDate { get; set; }

    public int? SupplierId { get; set; }

    public Supplier? Supplier { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public string? Notes { get; set; }

    public ICollection<PurchaseItem> Items
    { get; set; } = new List<PurchaseItem>();
}