using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Reports;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class SupplierReportView : UserControl
{
    public SupplierReportView()
    {
        InitializeComponent();

        DataContext = new SupplierReportViewModel(
            App.Services.GetRequiredService<ISupplierRepository>());
    }
}