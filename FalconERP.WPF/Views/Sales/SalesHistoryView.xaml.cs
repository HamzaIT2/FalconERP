using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Sales;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FalconERP.WPF.Views.Sales;

public partial class SalesHistoryView : UserControl
{
    public SalesHistoryView()
    {
        InitializeComponent();

        var saleRepository =
            App.Services.GetRequiredService<ISaleRepository>();

        DataContext =
            new SalesHistoryViewModel(saleRepository);
    }
    private void DataGrid_MouseDoubleClick(
    object sender,
    MouseButtonEventArgs e)
    {
        if (DataContext is not SalesHistoryViewModel vm)
            return;

        if (vm.SelectedSale == null)
            return;

        var window = new InvoiceDetailsWindow(vm.SelectedSale.Id);

        window.Owner = Window.GetWindow(this);

        window.ShowDialog();
    }
}