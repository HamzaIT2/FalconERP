using FalconERP.Application.DTOs;
using FalconERP.Application.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using FalconERP.WPF.ViewModels.Shared;
namespace FalconERP.WPF.ViewModels.Sales;

public class SalesHistoryViewModel : BaseViewModel
{
    private readonly ISaleRepository _saleRepository;
    private List<SaleHistoryDto> _allSales = new();

    public ObservableCollection<SaleHistoryDto> Sales
    { get; set; } = new();

    public int TotalInvoices
    {
        get => Sales.Count;
    }

    public decimal TotalSales
    {
        get => Sales.Sum(x => x.TotalAmount);
    }

    public decimal TotalDebts
    {
        get => Sales.Sum(x => x.RemainingAmount);
    }

    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();

            FilterSales();
        }
    }

    public SalesHistoryViewModel(
        ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;

        LoadSales();
    }

    private void LoadSales()
    {
        _allSales = _saleRepository
            .GetHistoryAsync()
            .GetAwaiter()
            .GetResult();

        Sales.Clear();

        foreach (var sale in _allSales)
        {
            Sales.Add(sale);
        }
        OnPropertyChanged(nameof(TotalInvoices));
        OnPropertyChanged(nameof(TotalSales));
        OnPropertyChanged(nameof(TotalDebts));
    }

    private void FilterSales()
    {
        Sales.Clear();

        var result = _allSales
            .Where(x =>
                x.CustomerName.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Id.ToString().Contains(SearchText));

        foreach (var sale in result)
        {
            Sales.Add(sale);
        }

        OnPropertyChanged(nameof(TotalInvoices));
        OnPropertyChanged(nameof(TotalSales));
        OnPropertyChanged(nameof(TotalDebts));
    }









    private SaleHistoryDto? _selectedSale;

    public SaleHistoryDto? SelectedSale
    {
        get => _selectedSale;
        set
        {
            _selectedSale = value;
            OnPropertyChanged();
        }
    }



}