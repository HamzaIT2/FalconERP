using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Reports;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class PurchaseReportView : UserControl
{
    public PurchaseReportView()
    {
        InitializeComponent();

        DataContext = new PurchaseReportViewModel(
            App.Services.GetRequiredService<IPurchaseRepository>());
    }
}