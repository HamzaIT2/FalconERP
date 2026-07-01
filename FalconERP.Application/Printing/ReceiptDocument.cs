namespace FalconERP.Application.Printing;

public class ReceiptDocument
{
    public string StoreName { get; set; } = "";

    public string Phone { get; set; } = "";

    public string Address { get; set; } = "";

    public string InvoiceNumber { get; set; } = "";

    public DateTime InvoiceDate { get; set; }

    public string CustomerName { get; set; } = "";

    public List<ReceiptItem> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public string Footer { get; set; } = "";
}