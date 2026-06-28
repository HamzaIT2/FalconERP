using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Inventorys;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Sales;

public partial class StockBalanceView : UserControl
{
    public StockBalanceView()
    {
        InitializeComponent();

        var inventoryRepository =
            App.Services.GetRequiredService<IInventoryTransactionRepository>();

        DataContext =
            new StockBalanceViewModel(inventoryRepository);
    }
}