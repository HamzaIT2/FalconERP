using FalconERP.Application.Interfaces;
using FalconERP.Infrastructure.Printing;
using FalconERP.WPF.ViewModels.Sales;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Sales;

public partial class SalesView : UserControl
{
    public SalesView()
    {
        InitializeComponent();

        DataContext = new SalesViewModel(
            App.Services.GetRequiredService<ISaleRepository>(),
            App.Services.GetRequiredService<IProductRepository>(),
            App.Services.GetRequiredService<IProductUnitRepository>(),
            App.Services.GetRequiredService<IInventoryTransactionRepository>(),
            App.Services.GetRequiredService<ICustomerRepository>(),
            App.Services.GetRequiredService<ISystemSettingsRepository>(),
            App.Services.GetRequiredService<ReceiptFactory>(),
            App.Services.GetRequiredService<ReceiptPrinterService>());

    }
}