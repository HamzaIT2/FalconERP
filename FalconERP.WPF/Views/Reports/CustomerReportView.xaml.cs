using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Reports;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class CustomerReportView : UserControl
{
    public CustomerReportView()
    {
        InitializeComponent();

        DataContext = new CustomerReportViewModel(
            App.Services.GetRequiredService<ICustomerRepository>());
    }
}