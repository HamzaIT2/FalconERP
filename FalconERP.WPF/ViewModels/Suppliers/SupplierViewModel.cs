using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Suppliers;

public class SupplierViewModel : BaseViewModel
{
    private readonly ISupplierRepository _supplierRepository;

    public ObservableCollection<Supplier> Suppliers
    { get; set; } = new();

    public ICommand AddSupplierCommand { get; }

    private string _supplierName = string.Empty;

    public string SupplierName
    {
        get => _supplierName;
        set => SetProperty(ref _supplierName, value);
    }

    private string _phone = string.Empty;

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    private string _address = string.Empty;

    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    private string _notes = string.Empty;

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }
    private Supplier? _selectedSupplier;

    public Supplier? SelectedSupplier
    {
        get => _selectedSupplier;
        set
        {
            if (SetProperty(ref _selectedSupplier, value))
            {
                if (value == null)
                    return;

                SupplierName = value.Name;
                Phone = value.Phone ?? "";
                Address = value.Address ?? "";
                Notes = value.Notes ?? "";
            }
        }
    }

    public SupplierViewModel(
        ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;

        AddSupplierCommand =  new RelayCommand(AddSupplier);

        UpdateSupplierCommand =  new RelayCommand(UpdateSupplier);

        DeleteSupplierCommand =  new RelayCommand(DeleteSupplier);

        LoadSuppliers();
    }
    public ICommand UpdateSupplierCommand { get; }

    public ICommand DeleteSupplierCommand { get; }

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
    }

    private void AddSupplier(object? parameter)
    {
        if (string.IsNullOrWhiteSpace(SupplierName))
        {
            MessageBox.Show("يرجى إدخال اسم المورد.");
            return;
        }

        var exists = Suppliers.Any(x =>
            x.Name.Trim().ToLower() ==
            SupplierName.Trim().ToLower());

        if (exists)
        {
            MessageBox.Show("اسم المورد موجود مسبقًا.");
            return;
        }

        var supplier = new Supplier
        {
            Name = SupplierName,
            Phone = Phone,
            Address = Address,
            Notes = Notes,
            Balance = 0,
            IsActive = true
        };

        _supplierRepository
            .AddAsync(supplier)
            .GetAwaiter()
            .GetResult();

        SupplierName = "";
        Phone = "";
        Address = "";
        Notes = "";

        SelectedSupplier = null;

        LoadSuppliers();

        MessageBox.Show("تمت إضافة المورد بنجاح.");
    }
    private void UpdateSupplier(object? parameter)
    {
        if (SelectedSupplier == null)
            return;

        SelectedSupplier.Name = SupplierName;
        SelectedSupplier.Phone = Phone;
        SelectedSupplier.Address = Address;
        SelectedSupplier.Notes = Notes;

        _supplierRepository
            .UpdateAsync(SelectedSupplier)
            .GetAwaiter()
            .GetResult();

        LoadSuppliers();

        SupplierName = "";
        Phone = "";
        Address = "";
        Notes = "";

        SelectedSupplier = null;

        MessageBox.Show("تم تحديث المورد.");
    }
    private void DeleteSupplier(object? parameter)
    {
        if (SelectedSupplier == null)
            return;

        var result = MessageBox.Show(
            "هل تريد حذف المورد؟",
            "تأكيد",
            MessageBoxButton.YesNo);

        if (result != MessageBoxResult.Yes)
            return;

        _supplierRepository
            .DeleteAsync(SelectedSupplier.Id)
            .GetAwaiter()
            .GetResult();

        LoadSuppliers();

        SupplierName = "";
        Phone = "";
        Address = "";
        Notes = "";

        SelectedSupplier = null;

        LoadSuppliers();
    }
}