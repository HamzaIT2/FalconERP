using FalconERP.WPF.Views.Category;
using FalconERP.WPF.Views.Customers;
using FalconERP.WPF.Views.Inventory;
using FalconERP.WPF.Views.Products;
using FalconERP.WPF.Views.Purchases;
using FalconERP.WPF.Views.Reports;
using FalconERP.WPF.Views.Sales;
using FalconERP.WPF.Views.Settings;
using FalconERP.WPF.Views.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace FalconERP.WPF.Views.Dashbord;

/// <summary>
/// Interaction logic for DashboardView.xaml
/// </summary>
public partial class DashboardView : Window
{
    public DashboardView()
    {
        InitializeComponent();

        MainContent.Content = new HomeView();
    }

    private void CategoriesButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainContent.Content = new CategoryView();
    }

    private void ProductsButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        MainContent.Content = new ProductView();
    }
    private void ProductUnitsButton_Click(
      object sender,
       RoutedEventArgs e)
    {
        MainContent.Content = new ProductUnitView();
    }

    private void InventoryButton_Click(object sender,RoutedEventArgs e)
    {
        try
        {
            MainContent.Content = new InventoryView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.ToString(),
                "Inventory Error");
        }
    }
    private void StockBalanceButton_Click(object sender,RoutedEventArgs e)
    {
        MainContent.Content = new StockBalanceView();
    }

    private void SalesButton_Click(object sender,RoutedEventArgs e)
    {
        MainContent.Content = new SalesView();
    }

    private void CustomersButton_Click(object sender,RoutedEventArgs e)
    {
        MainContent.Content = new CustomerView();
    }

    private void SalesHistoryButton_Click(object sender,RoutedEventArgs e)
    {
        MainContent.Content = new SalesHistoryView();
    }


    private void SuppliersButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        MainContent.Content = new SupplierView();
    }


    private void PurchaseButton_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new PurchaseView();
    }

    private void PurchaseHistoryButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        MainContent.Content = new PurchaseHistoryView();
    }
    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new HomeView();
    }

    private void ReportsButton_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new ReportsView();
    }

    private void SettingsButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        MainContent.Content = new SettingsView();
    }

}
