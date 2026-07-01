using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Reports;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class InventoryReportView : UserControl
{
    public InventoryReportView()
    {
        InitializeComponent();

        DataContext = new InventoryReportViewModel(
            App.Services.GetRequiredService<IInventoryTransactionRepository>());
    }
}