using System.Windows;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Reports;

public partial class ReportsView : UserControl
{
    public ReportsView()
    {
        InitializeComponent();
    }

    private void SalesReport_Click(
        object sender,
        RoutedEventArgs e)
    {
        var window = new Window
        {
            Title = "تقرير المبيعات",
            Width = 1300,
            Height = 800,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new SalesReportView()
        };

        window.ShowDialog();
    }
    private void PurchaseReport_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window = new Window
        {
            Title = "تقرير المشتريات",
            Width = 1300,
            Height = 800,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new PurchaseReportView()
        };

        window.ShowDialog();
    }
    private void InventoryReport_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window = new Window
        {
            Title = "تقرير المخزون",
            Width = 1200,
            Height = 750,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new InventoryReportView()
        };

        window.ShowDialog();
    }
    private void CustomerReport_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window = new Window
        {
            Title = "تقرير العملاء",
            Width = 1200,
            Height = 750,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new CustomerReportView()
        };

        window.ShowDialog();
    }
    private void SupplierReport_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window = new Window
        {
            Title = "تقرير الموردين",
            Width = 1200,
            Height = 750,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = new SupplierReportView()
        };

        window.ShowDialog();
    }
}