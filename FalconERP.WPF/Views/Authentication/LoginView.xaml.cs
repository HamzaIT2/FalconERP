using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FalconERP.WPF.Views.Authentication;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();

        var authService =
            App.Services.GetRequiredService<IAuthService>();

        DataContext = new LoginViewModel(authService);
    }
}