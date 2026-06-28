using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Inventorys;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Inventory;

public partial class InventoryView : UserControl
{
    public InventoryView()
    {
        InitializeComponent();

        var inventoryRepository =
            App.Services.GetRequiredService<IInventoryTransactionRepository>();

        var productRepository =
            App.Services.GetRequiredService<IProductRepository>();

        var productUnitRepository =
            App.Services.GetRequiredService<IProductUnitRepository>();

        DataContext = new InventoryViewModel(
            inventoryRepository,
            productRepository,
            productUnitRepository);
    }
}