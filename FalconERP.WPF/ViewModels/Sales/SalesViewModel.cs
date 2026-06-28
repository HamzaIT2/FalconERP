using FalconERP.Application.DTOs;
using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace FalconERP.WPF.ViewModels.Sales;

using FalconERP.WPF.ViewModels.Shared;
using System.Linq;
using System.Windows;

public class SalesViewModel : BaseViewModel
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IInventoryTransactionRepository _inventoryRepository;
    private readonly ICustomerRepository _customerRepository;
    public ObservableCollection<Product> Products { get; set; }
        = new();
    public ObservableCollection<Customer> Customers
    { get; set; } = new();


    public ObservableCollection<ProductUnit> ProductUnits { get; set; }
        = new();



    public ObservableCollection<SaleItemDto> SaleItems
    { get; set; } = new();
    public ICommand AddItemCommand { get; }

    public ICommand SaveInvoiceCommand { get; }
    public ICommand RemoveItemCommand { get; }



    private Product? _selectedProduct;

    private Customer? _selectedCustomer;

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
            OnPropertyChanged();
        }
    }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();

            LoadUnits();
        }
    }

    private ProductUnit? _selectedUnit;

    public ProductUnit? SelectedUnit
    {
        get => _selectedUnit;
        set
        {
            _selectedUnit = value;
            OnPropertyChanged();
        }
    }
    private decimal _quantity;

    public decimal Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    private decimal _price;


    private decimal _paidAmount;

    public decimal PaidAmount
    {
        get => _paidAmount;
        set
        {
            _paidAmount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(RemainingAmount));
        }
    }

    public decimal RemainingAmount
    {
        get => GrandTotal - PaidAmount;
    }



    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    public SalesViewModel(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        IInventoryTransactionRepository inventoryRepository,
        ICustomerRepository customerRepository
        )
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _inventoryRepository = inventoryRepository;
        _customerRepository = customerRepository;
        AddItemCommand =new RelayCommand(AddItem);
        SaveInvoiceCommand = new RelayCommand(SaveInvoice);
        RemoveItemCommand =new RelayCommand(RemoveItem);

        LoadProducts();
        LoadCustomers();
    }

    private void LoadProducts()
    {
        var products = _productRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(product);
        }
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

    private void LoadUnits()
    {
        ProductUnits.Clear();

        if (SelectedProduct is null)
            return;

        var units = _productUnitRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult()
            .Where(x => x.ProductId == SelectedProduct.Id);

        foreach (var unit in units)
        {
            ProductUnits.Add(unit);
        }
    }

    private void AddItem(object? parameter)
    {
        if (SelectedProduct is null)
            return;

        if (SelectedUnit is null)
            return;

        if (Quantity <= 0)
            return;

        var item = new SaleItemDto
        {
            ProductId = SelectedProduct.Id,
            ProductUnitId = SelectedUnit.Id,

            ProductName = SelectedProduct.Name,
            UnitName = SelectedUnit.UnitName,

            Quantity = Quantity,
            Price = Price
        };

        SaleItems.Add(item);

        OnPropertyChanged(nameof(GrandTotal));
        OnPropertyChanged(nameof(RemainingAmount));

        Quantity = 0;
        Price = 0;

        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));
    }

    private void SaveInvoice(object? parameter)


    {


        if (SaleItems.Count == 0)
            return;




        if (PaidAmount > GrandTotal)
        {
            MessageBox.Show(
                "المبلغ المدفوع أكبر من إجمالي الفاتورة.");

            return;
        }

        if (RemainingAmount > 0 && SelectedCustomer == null)
        {
            MessageBox.Show(
                "يجب اختيار عميل عند البيع الآجل.");

            return;
        }








        var stockBalances = _inventoryRepository
            .GetStockBalancesAsync()
            .GetAwaiter()
            .GetResult();

        foreach (var item in SaleItems)
        {
            var balance = stockBalances
                .FirstOrDefault(x =>
                    x.ProductName == item.ProductName);

            if (balance is null)
                return;

            if (balance.Balance < item.Quantity)
            {
                MessageBox.Show(
                    $"المخزون غير كافٍ للمنتج {item.ProductName}");

                return;
            }
        }

        var sale = new Sale
        {
            SaleDate = DateTime.Now,

            CustomerId = SelectedCustomer?.Id,

            TotalAmount = GrandTotal,


            PaidAmount = PaidAmount,

            RemainingAmount = RemainingAmount,

            Notes = "فاتورة مبيعات"
        };

        foreach (var item in SaleItems)
        {
            sale.Items.Add(new SaleItem
            {
                ProductId = item.ProductId,
                ProductUnitId = item.ProductUnitId,
                Quantity = item.Quantity,
                Price = item.Price,
                Total = item.Total
            });
        }

        _saleRepository
            .AddAsync(sale)
            .GetAwaiter()
            .GetResult();

        if (SelectedCustomer != null && RemainingAmount > 0)
        {
            SelectedCustomer.Balance += RemainingAmount;

            _customerRepository
                .UpdateAsync(SelectedCustomer)
                .GetAwaiter()
                .GetResult();
        }

        foreach (var item in SaleItems)
        {
            var transaction = new InventoryTransaction
            {
                ProductId = item.ProductId,
                ProductUnitId = item.ProductUnitId,
                Quantity = item.Quantity,
                TransactionType = "إخراج",
                TransactionDate = DateTime.Now,
                Notes = $"فاتورة بيع رقم {sale.Id}"
            };

            _inventoryRepository
                .AddAsync(transaction)
                .GetAwaiter()
                .GetResult();
        }

        SaleItems.Clear();

        PaidAmount = 0;

        OnPropertyChanged(nameof(PaidAmount));
        OnPropertyChanged(nameof(RemainingAmount));
        OnPropertyChanged(nameof(GrandTotal));


        SelectedCustomer = null;
        SelectedProduct = null;
        SelectedUnit = null;

        ProductUnits.Clear();

        Quantity = 0;
        Price = 0;

        OnPropertyChanged(nameof(SelectedCustomer));
        OnPropertyChanged(nameof(SelectedProduct));
        OnPropertyChanged(nameof(SelectedUnit));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));




    }

    public decimal GrandTotal
    {
        get => SaleItems.Sum(x => x.Total);
    }
    private SaleItemDto? _selectedSaleItem;

    public SaleItemDto? SelectedSaleItem
    {
        get => _selectedSaleItem;
        set
        {
            _selectedSaleItem = value;
            OnPropertyChanged();
        }
    }

    private void RemoveItem(object? parameter)
    {
        if (SelectedSaleItem is null)
            return;

        SaleItems.Remove(SelectedSaleItem);

        OnPropertyChanged(nameof(GrandTotal));
        OnPropertyChanged(nameof(RemainingAmount));
    }

}