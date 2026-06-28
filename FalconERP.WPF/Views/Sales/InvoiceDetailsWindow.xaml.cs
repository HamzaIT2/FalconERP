using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Invoices;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FalconERP.WPF.Views.Sales;

public partial class InvoiceDetailsWindow : Window
{
    public InvoiceDetailsWindow(int saleId)
    {
        InitializeComponent();

        var repository =
            App.Services.GetRequiredService<ISaleRepository>();

        DataContext =
            new InvoiceDetailsViewModel(repository, saleId);
    }
}