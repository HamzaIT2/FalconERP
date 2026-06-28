using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Customers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Customers;

public partial class CustomerView : UserControl
{
    public CustomerView()
    {
        InitializeComponent();

        var customerRepository =
            App.Services.GetRequiredService<ICustomerRepository>();

        DataContext =
            new CustomerViewModel(customerRepository);
    }
}