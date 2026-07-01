using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Reports;

public class CustomerReportViewModel : BaseViewModel
{
    private readonly ICustomerRepository _customerRepository;

    public ObservableCollection<Customer> Customers { get; }
        = new();

    public ICommand RefreshCommand { get; }

    public CustomerReportViewModel(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RefreshCommand = new RelayCommand(Refresh);

        LoadCustomers();
    }

    private void LoadCustomers()
    {
        var customers = _customerRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Customers.Clear();

        foreach (var customer in customers)
        {
            Customers.Add(customer);
        }

        OnPropertyChanged(nameof(TotalCustomers));
        OnPropertyChanged(nameof(TotalBalance));
    }

    private void Refresh(object? parameter)
    {
        LoadCustomers();
    }

    public int TotalCustomers =>
        Customers.Count;

    public decimal TotalBalance =>
        Customers.Sum(x => x.Balance);
}