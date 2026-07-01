using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Reports;

public class SalesReportViewModel : BaseViewModel
{
    private readonly IReportsRepository _reportsRepository;
    private readonly IExcelExportService _excelExportService;

    public ObservableCollection<Sale> Sales { get; }
        = new();

    public ICommand FilterCommand { get; }

    public ICommand ExportExcelCommand { get; }

    private DateTime _fromDate = DateTime.Today.AddMonths(-1);

    public DateTime FromDate
    {
        get => _fromDate;
        set => SetProperty(ref _fromDate, value);
    }

    private DateTime _toDate = DateTime.Today;

    public DateTime ToDate
    {
        get => _toDate;
        set => SetProperty(ref _toDate, value);
    }

    private string _searchText = "";

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public decimal TotalSales => Sales.Sum(x => x.TotalAmount);

    public decimal TotalPaid => Sales.Sum(x => x.PaidAmount);

    public decimal TotalRemaining => Sales.Sum(x => x.RemainingAmount);

    public SalesReportViewModel(
        IReportsRepository reportsRepository,
        IExcelExportService excelExportService)
    {
        _reportsRepository = reportsRepository;
        _excelExportService = excelExportService;

        FilterCommand = new RelayCommand(FilterSales);
        ExportExcelCommand = new RelayCommand(ExportExcel);

        LoadSales();
    }

    private void LoadSales()
    {
        var sales = _reportsRepository
            .GetSalesReportAsync()
            .GetAwaiter()
            .GetResult();

        Sales.Clear();

        foreach (var sale in sales)
        {
            Sales.Add(sale);
        }

        OnPropertyChanged(nameof(TotalSales));
        OnPropertyChanged(nameof(TotalPaid));
        OnPropertyChanged(nameof(TotalRemaining));
    }

    private void FilterSales(object? parameter)
    {
        var sales = _reportsRepository
            .GetSalesReportAsync()
            .GetAwaiter()
            .GetResult();

        var result = sales.Where(x =>
            x.SaleDate.Date >= FromDate.Date &&
            x.SaleDate.Date <= ToDate.Date);

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            result = result.Where(x =>
                x.Customer != null &&
                x.Customer.Name.Contains(SearchText));
        }

        Sales.Clear();

        foreach (var sale in result)
        {
            Sales.Add(sale);
        }

        OnPropertyChanged(nameof(TotalSales));
        OnPropertyChanged(nameof(TotalPaid));
        OnPropertyChanged(nameof(TotalRemaining));
    }

    private void ExportExcel(object? parameter)
    {
        var dialog = new Microsoft.Win32.SaveFileDialog();

        dialog.Filter = "Excel File|*.xlsx";
        dialog.FileName = $"تقرير_المبيعات_{DateTime.Now:yyyyMMdd}.xlsx";

        if (dialog.ShowDialog() != true)
            return;

        _excelExportService
            .ExportSalesReportAsync(
                dialog.FileName,
                FromDate,
                ToDate,
                SearchText)
            .GetAwaiter()
            .GetResult();

        MessageBox.Show("تم تصدير التقرير بنجاح.");
    }
}