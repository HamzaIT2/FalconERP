using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Purchases;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Purchases;

public partial class PurchaseView : UserControl
{
    public PurchaseView()
    {
        InitializeComponent();

        DataContext = new PurchaseViewModel(
            App.Services.GetRequiredService<IPurchaseRepository>(),
            App.Services.GetRequiredService<IProductRepository>(),
            App.Services.GetRequiredService<IProductUnitRepository>(),
            App.Services.GetRequiredService<IInventoryTransactionRepository>(),
            App.Services.GetRequiredService<ISupplierRepository>());
    }
}