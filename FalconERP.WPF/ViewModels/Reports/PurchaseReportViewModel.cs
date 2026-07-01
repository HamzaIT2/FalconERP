using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Reports;

public class PurchaseReportViewModel : BaseViewModel
{
    private readonly IPurchaseRepository _purchaseRepository;

    public ObservableCollection<Purchase> Purchases { get; }
        = new();

    public ICommand FilterCommand { get; }

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

    public decimal TotalPurchases =>
        Purchases.Sum(x => x.TotalAmount);

    public decimal TotalPaid =>
        Purchases.Sum(x => x.PaidAmount);

    public decimal TotalRemaining =>
        Purchases.Sum(x => x.RemainingAmount);

    public PurchaseReportViewModel(
        IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;

        FilterCommand =
            new RelayCommand(FilterPurchases);

        LoadPurchases();
    }

    private void LoadPurchases()
    {
        var purchases = _purchaseRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Purchases.Clear();

        foreach (var purchase in purchases)
        {
            Purchases.Add(purchase);
        }

        OnPropertyChanged(nameof(TotalPurchases));
        OnPropertyChanged(nameof(TotalPaid));
        OnPropertyChanged(nameof(TotalRemaining));
    }

    private void FilterPurchases(object? parameter)
    {
        var purchases = _purchaseRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        var result = purchases.Where(x =>
            x.PurchaseDate.Date >= FromDate.Date &&
            x.PurchaseDate.Date <= ToDate.Date);

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            result = result.Where(x =>
                x.Supplier != null &&
                x.Supplier.Name.Contains(SearchText));
        }

        Purchases.Clear();

        foreach (var purchase in result)
        {
            Purchases.Add(purchase);
        }

        OnPropertyChanged(nameof(TotalPurchases));
        OnPropertyChanged(nameof(TotalPaid));
        OnPropertyChanged(nameof(TotalRemaining));
    }
}