using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Customers;

public class CustomerViewModel : BaseViewModel
{
    private readonly ICustomerRepository _customerRepository;

    public ObservableCollection<Customer> Customers
    { get; set; } = new();

    public ICommand AddCustomerCommand { get; }

    private string _customerName = string.Empty;

    public string CustomerName
    {
        get => _customerName;
        set
        {
            _customerName = value;
            OnPropertyChanged();
        }
    }

    private string _phone = string.Empty;

    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            OnPropertyChanged();
        }
    }

    private string _address = string.Empty;

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
        }
    }

    public CustomerViewModel(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        AddCustomerCommand =
            new RelayCommand(AddCustomer);

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
    }

    private void AddCustomer(object? parameter)
    {
        if (string.IsNullOrWhiteSpace(CustomerName))
            return;

        var customer = new Customer
        {
            Name = CustomerName,
            Phone = Phone,
            Address = Address,
            Balance = 0
        };

        _customerRepository
            .AddAsync(customer)
            .GetAwaiter()
            .GetResult();

        CustomerName = "";
        Phone = "";
        Address = "";

        LoadCustomers();

        OnPropertyChanged(nameof(CustomerName));
        OnPropertyChanged(nameof(Phone));
        OnPropertyChanged(nameof(Address));
    }


}