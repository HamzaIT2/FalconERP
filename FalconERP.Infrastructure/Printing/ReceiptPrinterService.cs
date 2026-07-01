using System.Text;
using FalconERP.Application.Printing;

namespace FalconERP.Infrastructure.Printing;

public class ReceiptPrinterService
{
    public byte[] BuildReceiptBytes(ReceiptDocument receipt)
    {
        using var stream = new MemoryStream();

        void Write(byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        Write(EscPosCommands.Initialize);

        Write(EscPosCommands.AlignCenter);
        Write(EscPosCommands.BoldOn);
        Write(EscPosCommands.DoubleHeightWidth);
        Write(EscPosCommands.Text(receipt.StoreName));

        Write(EscPosCommands.NormalSize);
        Write(EscPosCommands.BoldOff);

        Write(EscPosCommands.Text(receipt.Phone));
        Write(EscPosCommands.Text(receipt.Address));

        Write(EscPosCommands.AlignLeft);

        Write(EscPosCommands.Text("--------------------------------"));

        Write(EscPosCommands.Text($"الفاتورة : {receipt.InvoiceNumber}"));
        Write(EscPosCommands.Text($"التاريخ : {receipt.InvoiceDate:yyyy/MM/dd HH:mm}"));
        Write(EscPosCommands.Text($"العميل : {receipt.CustomerName}"));

        Write(EscPosCommands.Text("--------------------------------"));

        foreach (var item in receipt.Items)
        {
            Write(EscPosCommands.Text(item.ProductName));

            Write(EscPosCommands.Text(
                $"{item.Quantity} × {item.Price:0.##} = {item.Total:0.##}"));
        }

        Write(EscPosCommands.Text("--------------------------------"));

        Write(EscPosCommands.BoldOn);

        Write(EscPosCommands.Text($"الإجمالي : {receipt.TotalAmount:0.##}"));

        Write(EscPosCommands.Text($"المدفوع : {receipt.PaidAmount:0.##}"));

        Write(EscPosCommands.Text($"المتبقي : {receipt.RemainingAmount:0.##}"));

        Write(EscPosCommands.BoldOff);

        Write(EscPosCommands.Text("--------------------------------"));

        Write(EscPosCommands.AlignCenter);

        Write(EscPosCommands.Text(receipt.Footer));

        Write(EscPosCommands.FeedLines);

        Write(EscPosCommands.CutPaper);

        return stream.ToArray();
    }
}