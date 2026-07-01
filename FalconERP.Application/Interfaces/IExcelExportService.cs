namespace FalconERP.Application.Interfaces;

public interface IExcelExportService
{
    Task ExportSalesReportAsync(
    string filePath,
    DateTime fromDate,
    DateTime toDate,
    string searchText);
}