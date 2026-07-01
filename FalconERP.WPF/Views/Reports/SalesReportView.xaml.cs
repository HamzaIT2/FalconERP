using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Reports;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class SalesReportView : UserControl
{
    public SalesReportView()
    {
        InitializeComponent();

        DataContext = new SalesReportViewModel(
            App.Services.GetRequiredService<IReportsRepository>(),
            App.Services.GetRequiredService<IExcelExportService>());
    }
}