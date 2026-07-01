using FalconERP.Application.Printing;
using FalconERP.Domain.Entities;

namespace FalconERP.Infrastructure.Printing;

public class ReceiptBuilder
{
    public ReceiptDocument BuildSaleReceipt(
        Sale sale,
        string storeName,
        string phone,
        string address,
        string footer)
    {
        var document = new ReceiptDocument
        {
            StoreName = storeName,
            Phone = phone,
            Address = address,

            InvoiceNumber = sale.InvoiceNumber,

            InvoiceDate = sale.SaleDate,

            CustomerName = sale.Customer?.Name ?? "زبون نقدي",

            TotalAmount = sale.TotalAmount,

            PaidAmount = sale.PaidAmount,

            RemainingAmount = sale.RemainingAmount,

            Footer = footer
        };

        foreach (var item in sale.Items)
        {
            document.Items.Add(new ReceiptItem
            {
                ProductName = item.Product?.Name ?? "",

                UnitName = item.ProductUnit?.UnitName ?? "",

                Quantity = item.Quantity,

                Price = item.Price,

                Total = item.Total
            });
        }

        return document;
    }
}