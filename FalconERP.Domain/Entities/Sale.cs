using FalconERP.Domain.Common;
namespace FalconERP.Domain.Entities;

public class Sale : BaseEntity
{
    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public string? Notes { get; set; }

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public ICollection<SaleItem> Items { get; set; }
        = new List<SaleItem>();

}