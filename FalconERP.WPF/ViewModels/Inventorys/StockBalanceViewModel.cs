using FalconERP.Application.DTOs;
using FalconERP.Application.Interfaces;
using System.Collections.ObjectModel;

namespace FalconERP.WPF.ViewModels.Inventorys;

public class StockBalanceViewModel
{
    private readonly IInventoryTransactionRepository _inventoryRepository;

    public ObservableCollection<StockBalanceDto> Balances
    { get; set; } = new();

    public StockBalanceViewModel(
        IInventoryTransactionRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;

        LoadBalances();
    }

    private void LoadBalances()
    {
        var balances = _inventoryRepository
            .GetStockBalancesAsync()
            .GetAwaiter()
            .GetResult();

        Balances.Clear();

        foreach (var item in balances)
        {
            Balances.Add(item);
        }
    }
}