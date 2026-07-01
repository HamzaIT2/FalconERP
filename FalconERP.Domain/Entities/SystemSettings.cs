namespace FalconERP.Domain.Entities;

public class SystemSettings
{
    public int Id { get; set; }

    public string StoreName { get; set; } = "";

    public string OwnerName { get; set; } = "";

    public string Phone { get; set; } = "";

    public string Email { get; set; } = "";

    public string Address { get; set; } = "";

    public string Currency { get; set; } = "IQD";

    public string ReceiptFooter { get; set; } =
        "شكراً لتعاملكم معنا";

    public string PrinterType { get; set; } =
        "80mm";

    public string? LogoPath { get; set; }
    public string PrinterName { get; set; } = "";

    public bool AutoPrintReceipt { get; set; } = true;

    public int PaperWidth { get; set; } = 80;
}