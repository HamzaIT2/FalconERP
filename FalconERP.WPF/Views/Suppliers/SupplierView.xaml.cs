using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels;
using FalconERP.WPF.ViewModels.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Suppliers;

public partial class SupplierView : UserControl
{
    public SupplierView()
    {
        InitializeComponent();

        DataContext = new SupplierViewModel(
            App.Services.GetRequiredService<ISupplierRepository>());
    }
}