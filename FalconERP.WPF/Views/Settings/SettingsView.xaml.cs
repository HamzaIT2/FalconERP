using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Settings;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();

        DataContext = new SettingsViewModel(
            App.Services.GetRequiredService<ISystemSettingsRepository>());
    }
}