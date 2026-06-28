using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Products;

public class ProductUnitViewModel : BaseViewModel
{
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IProductRepository _productRepository;

    public ObservableCollection<Product> Products { get; set; }
        = new();

    public ObservableCollection<ProductUnit> ProductUnits { get; set; }
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

    private string _unitName = string.Empty;

    public string UnitName
    {
        get => _unitName;
        set
        {
            _unitName = value;
            OnPropertyChanged();
        }
    }

    private decimal _conversionFactor = 1;

    public decimal ConversionFactor
    {
        get => _conversionFactor;
        set
        {
            _conversionFactor = value;
            OnPropertyChanged();
        }
    }

    private decimal _purchasePrice;

    public decimal PurchasePrice
    {
        get => _purchasePrice;
        set
        {
            _purchasePrice = value;
            OnPropertyChanged();
        }
    }

    private decimal _wholesalePrice;

    public decimal WholesalePrice
    {
        get => _wholesalePrice;
        set
        {
            _wholesalePrice = value;
            OnPropertyChanged();
        }
    }

    private decimal _retailPrice;

    public decimal RetailPrice
    {
        get => _retailPrice;
        set
        {
            _retailPrice = value;
            OnPropertyChanged();
        }
    }

    private string _barcode = string.Empty;

    public string Barcode
    {
        get => _barcode;
        set
        {
            _barcode = value;
            OnPropertyChanged();
        }
    }

    private bool _isDefault;

    public bool IsDefault
    {
        get => _isDefault;
        set
        {
            _isDefault = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddUnitCommand { get; }

    public ProductUnitViewModel(
        IProductUnitRepository productUnitRepository,
        IProductRepository productRepository)
    {
        _productUnitRepository = productUnitRepository;
        _productRepository = productRepository;

        AddUnitCommand =
            new RelayCommand(AddUnit);

        LoadProducts();
        LoadUnits();
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

    private void AddUnit(object? parameter)
    {
        if (SelectedProduct is null)
            return;

        var unit = new ProductUnit
        {
            ProductId = SelectedProduct.Id,
            UnitName = UnitName,
            ConversionFactor = ConversionFactor,
            PurchasePrice = PurchasePrice,
            WholesalePrice = WholesalePrice,
            RetailPrice = RetailPrice,
            Barcode = Barcode,
            IsDefault = IsDefault
        };

        _productUnitRepository
            .AddAsync(unit)
            .GetAwaiter()
            .GetResult();

        UnitName = string.Empty;
        ConversionFactor = 1;
        PurchasePrice = 0;
        WholesalePrice = 0;
        RetailPrice = 0;
        Barcode = string.Empty;
        IsDefault = false;

        LoadUnits();
    }


}