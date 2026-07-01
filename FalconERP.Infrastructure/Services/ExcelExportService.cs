using ClosedXML.Excel;
using FalconERP.Application.Interfaces;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Services;

public class ExcelExportService : IExcelExportService
{
    private readonly AppDbContext _context;

    public ExcelExportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ExportSalesReportAsync(
        string filePath,
        DateTime fromDate,
        DateTime toDate,
        string searchText)
    {
        var query = _context.Sales.Include(x => x.Customer).AsQueryable();

        query = query.Where(x =>
            x.SaleDate.Date >= fromDate.Date &&
            x.SaleDate.Date <= toDate.Date);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(x =>
                x.Customer != null &&
                x.Customer.Name.Contains(searchText));
        }

        var sales = await query.ToListAsync();
   

        using var workbook = new XLWorkbook();

        var sheet = workbook.Worksheets.Add("تقرير المبيعات");

        sheet.Cell(1, 1).Value = "رقم الفاتورة";
        sheet.Cell(1, 2).Value = "التاريخ";
        sheet.Cell(1, 3).Value = "العميل";
        sheet.Cell(1, 4).Value = "الإجمالي";
        sheet.Cell(1, 5).Value = "المدفوع";
        sheet.Cell(1, 6).Value = "المتبقي";

        int row = 2;

        foreach (var sale in sales)
        {
            sheet.Cell(row, 1).Value = sale.Id;
            sheet.Cell(row, 2).Value = sale.SaleDate;
            sheet.Cell(row, 3).Value = sale.Customer?.Name;
            sheet.Cell(row, 4).Value = sale.TotalAmount;
            sheet.Cell(row, 5).Value = sale.PaidAmount;
            sheet.Cell(row, 6).Value = sale.RemainingAmount;

            row++;
        }

        workbook.SaveAs(filePath);
    }
}