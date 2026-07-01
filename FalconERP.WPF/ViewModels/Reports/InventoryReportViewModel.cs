using FalconERP.Application.Interfaces;
using FalconERP.Application.DTOs;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Reports;

public class InventoryReportViewModel : BaseViewModel
{
    private readonly IInventoryTransactionRepository _inventoryRepository;

    public ObservableCollection<StockBalanceDto> StockBalances
    { get; } = new();

    public ICommand RefreshCommand { get; }

    public InventoryReportViewModel(
        IInventoryTransactionRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;

        RefreshCommand = new RelayCommand(Refresh);

        LoadData();
    }

    private void LoadData()
    {
        var balances = _inventoryRepository
            .GetStockBalancesAsync()
            .GetAwaiter()
            .GetResult();

        StockBalances.Clear();

        foreach (var item in balances)
        {
            StockBalances.Add(item);
        }

        OnPropertyChanged(nameof(TotalProducts));
        OnPropertyChanged(nameof(TotalQuantity));
    }

    private void Refresh(object? obj)
    {
        LoadData();
    }

    public int TotalProducts =>
        StockBalances.Count;

    public decimal TotalQuantity =>
        StockBalances.Sum(x => x.Balance);
}