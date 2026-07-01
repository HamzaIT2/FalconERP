using FalconERP.Application.DTOs;
using FalconERP.Application.Printing;
using FalconERP.Domain.Entities;

namespace FalconERP.Infrastructure.Printing;

public class ReceiptFactory
{
    public ReceiptDocument CreateSaleReceipt(
        IEnumerable<SaleItemDto> items,
        Customer? customer,
        SystemSettings settings,
        string invoiceNumber,
        DateTime invoiceDate,
        decimal totalAmount,
        decimal paidAmount,
        decimal remainingAmount)
    {
        var receipt = new ReceiptDocument
        {
            StoreName = settings.StoreName,
            Phone = settings.Phone,
            Address = settings.Address,

            InvoiceNumber = invoiceNumber,
            InvoiceDate = invoiceDate,

            CustomerName = customer?.Name ?? "زبون نقدي",

            TotalAmount = totalAmount,
            PaidAmount = paidAmount,
            RemainingAmount = remainingAmount,

            Footer = settings.ReceiptFooter
        };

        foreach (var item in items)
        {
            receipt.Items.Add(new ReceiptItem
            {
                ProductName = item.ProductName,
                UnitName = item.UnitName,
                Quantity = item.Quantity,
                Price = item.Price,
                Total = item.Total
            });
        }

        return receipt;
    }
}