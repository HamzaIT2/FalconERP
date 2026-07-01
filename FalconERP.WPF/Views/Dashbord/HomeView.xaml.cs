using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Dashbord;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        DataContext = new HomeViewModel(
            App.Services.GetRequiredService<IDashboardRepository>());
    }
}