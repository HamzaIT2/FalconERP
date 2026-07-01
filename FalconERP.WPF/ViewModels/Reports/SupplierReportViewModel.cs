using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Reports;

public class SupplierReportViewModel : BaseViewModel
{
    private readonly ISupplierRepository _supplierRepository;

    public ObservableCollection<Supplier> Suppliers { get; }
        = new();

    public ICommand RefreshCommand { get; }

    public SupplierReportViewModel(
        ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;

        RefreshCommand = new RelayCommand(Refresh);

        LoadSuppliers();
    }

    private void LoadSuppliers()
    {
        var suppliers = _supplierRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Suppliers.Clear();

        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }

        OnPropertyChanged(nameof(TotalSuppliers));
        OnPropertyChanged(nameof(TotalBalance));
    }

    private void Refresh(object? parameter)
    {
        LoadSuppliers();
    }

    public int TotalSuppliers =>
        Suppliers.Count;

    public decimal TotalBalance =>
        Suppliers.Sum(x => x.Balance);
}