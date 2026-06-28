using FalconERP.Application.Interfaces;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using FalconERP.WPF.Views;
using FalconERP.WPF.Views.Authentication;
using FalconERP.WPF.Views.Dashbord;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Authentication;

public class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    private string _username = string.Empty;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password = string.Empty;

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;

        LoginCommand = new RelayCommand(Login);
    }

    private void Login(object? parameter)
    {
        var user = _authService
            .LoginAsync(Username, Password)
            .GetAwaiter()
            .GetResult();

        if (user is not null)
        {
            var dashboard = new DashboardView();

            dashboard.Show();

            System.Windows.Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(x => x is LoginView)
                ?.Close();
        }
        else
        {
            MessageBox.Show(
                "Invalid username or password",
                "Login Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}