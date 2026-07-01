using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;

namespace FalconERP.WPF.ViewModels.Purchases;

public class PurchaseHistoryViewModel : BaseViewModel
{
    private readonly IPurchaseRepository _purchaseRepository;

    public ObservableCollection<Purchase> Purchases { get; }
        = new();

    public PurchaseHistoryViewModel(
        IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;

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
    }
}