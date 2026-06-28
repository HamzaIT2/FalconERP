using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Inventorys;

public class   InventoryViewModel : BaseViewModel
{
    private readonly IInventoryTransactionRepository _inventoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public ObservableCollection<Product> Products { get; set; }
        = new();

    public ObservableCollection<ProductUnit> ProductUnits { get; set; }
        = new();

    public ObservableCollection<InventoryTransaction> Transactions { get; set; }
        = new();

    private Product? _selectedProduct;

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();
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

    private string _transactionType = "إدخال";

    public string TransactionType
    {
        get => _transactionType;
        set
        {
            _transactionType = value;
            OnPropertyChanged();
        }
    }

    private string _notes = string.Empty;

    public string Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveTransactionCommand { get; }

    public InventoryViewModel(
        IInventoryTransactionRepository inventoryRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository)
    {
        _inventoryRepository = inventoryRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;

        SaveTransactionCommand =
            new RelayCommand(SaveTransaction);

        LoadProducts();
        LoadUnits();
        LoadTransactions();
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

    private void LoadUnits()
    {
        var units = _productUnitRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        ProductUnits.Clear();

        foreach (var unit in units)
        {
            ProductUnits.Add(unit);
        }
    }

    private void LoadTransactions()
    {
        var transactions = _inventoryRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Transactions.Clear();

        foreach (var transaction in transactions)
        {
            Transactions.Add(transaction);
        }
    }

    private void SaveTransaction(object? parameter)
    {
        if (SelectedProduct is null)
            return;

        if (SelectedUnit is null)
            return;

        if (Quantity <= 0)
            return;

        var transaction = new InventoryTransaction
        {
            ProductId = SelectedProduct.Id,
            ProductUnitId = SelectedUnit.Id,
            Quantity = Quantity,
            TransactionType = TransactionType,
            TransactionDate = DateTime.Now,
            Notes = Notes
        };

        _inventoryRepository
            .AddAsync(transaction)
            .GetAwaiter()
            .GetResult();

        Quantity = 0;
        Notes = string.Empty;

        LoadTransactions();
    }
}