using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Purchases;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Purchases;

public partial class PurchaseHistoryView : UserControl
{
    public PurchaseHistoryView()
    {
        InitializeComponent();

        DataContext = new PurchaseHistoryViewModel(
            App.Services.GetRequiredService<IPurchaseRepository>());
    }
}