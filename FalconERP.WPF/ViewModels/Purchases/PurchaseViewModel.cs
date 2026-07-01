using FalconERP.Application.DTOs;
using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace FalconERP.WPF.ViewModels.Purchases;

public class PurchaseViewModel : BaseViewModel
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IInventoryTransactionRepository _inventoryRepository;
    private readonly ISupplierRepository _supplierRepository;

    public ObservableCollection<Product> Products { get; } = new();

    public ObservableCollection<ProductUnit> ProductUnits { get; } = new();

    public ObservableCollection<Supplier> Suppliers { get; } = new();

    public ObservableCollection<SaleItemDto> PurchaseItems { get; } = new();

    public ICommand AddItemCommand { get; }

    public ICommand SaveInvoiceCommand { get; }

    public ICommand RemoveItemCommand { get; }

    public PurchaseViewModel(
        IPurchaseRepository purchaseRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        IInventoryTransactionRepository inventoryRepository,
        ISupplierRepository supplierRepository)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _inventoryRepository = inventoryRepository;
        _supplierRepository = supplierRepository;


        AddItemCommand = new RelayCommand(AddItem);

        SaveInvoiceCommand = new RelayCommand(SaveInvoice);

        RemoveItemCommand = new RelayCommand(RemoveItem);

        LoadProducts();

        LoadSuppliers();






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

    private void LoadUnits()
    {
        ProductUnits.Clear();

        if (SelectedProduct == null)
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

    private Product? _selectedProduct;

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (SetProperty(ref _selectedProduct, value))
            {
                LoadUnits();
            }
        }
    }

    private ProductUnit? _selectedUnit;

    public ProductUnit? SelectedUnit
    {
        get => _selectedUnit;
        set => SetProperty(ref _selectedUnit, value);
    }

    private Supplier? _selectedSupplier;

    public Supplier? SelectedSupplier
    {
        get => _selectedSupplier;
        set => SetProperty(ref _selectedSupplier, value);
    }

    private decimal _quantity;

    public decimal Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value);
    }

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    private decimal _paidAmount;

    public decimal PaidAmount
    {
        get => _paidAmount;
        set
        {
            if (SetProperty(ref _paidAmount, value))
            {
                OnPropertyChanged(nameof(RemainingAmount));
            }
        }
    }

    public decimal RemainingAmount
    {
        get => GrandTotal - PaidAmount;
    }

    public decimal GrandTotal
    {
        get => PurchaseItems.Sum(x => x.Total);
    }


    private void AddItem(object? parameter)
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("يرجى اختيار المنتج.");
            return;
        }

        if (SelectedUnit == null)
        {
            MessageBox.Show("يرجى اختيار الوحدة.");
            return;
        }

        if (Quantity <= 0)
        {
            MessageBox.Show("يرجى إدخال كمية صحيحة.");
            return;
        }

        if (Price <= 0)
        {
            MessageBox.Show("يرجى إدخال سعر صحيح.");
            return;
        }

        var item = new SaleItemDto
        {
            ProductId = SelectedProduct.Id,
            ProductUnitId = SelectedUnit.Id,

            ProductName = SelectedProduct.Name,
            UnitName = SelectedUnit.UnitName,

            Quantity = Quantity,
            Price = Price
        };

        PurchaseItems.Add(item);

        OnPropertyChanged(nameof(GrandTotal));
        OnPropertyChanged(nameof(RemainingAmount));

        Quantity = 0;
        Price = 0;
        SelectedProduct = null;
        SelectedUnit = null;

        ProductUnits.Clear();

        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(SelectedProduct));
        OnPropertyChanged(nameof(SelectedUnit));
    }

    private void SaveInvoice(object? parameter)
    {
        if (PurchaseItems.Count == 0)
        {
            MessageBox.Show("الفاتورة فارغة.");
            return;
        }

        if (SelectedSupplier == null)
        {
            MessageBox.Show("يرجى اختيار المورد.");
            return;
        }

        if (PaidAmount > GrandTotal)
        {
            MessageBox.Show("المبلغ المدفوع أكبر من إجمالي الفاتورة.");
            return;
        }

        var purchase = new Purchase
        {
            PurchaseDate = DateTime.Now,

            SupplierId = SelectedSupplier.Id,

            TotalAmount = GrandTotal,

            PaidAmount = PaidAmount,

            RemainingAmount = RemainingAmount,

            Notes = "فاتورة شراء"
        };

        foreach (var item in PurchaseItems)
        {
            purchase.Items.Add(new PurchaseItem
            {
                ProductId = item.ProductId,
                ProductUnitId = item.ProductUnitId,
                Quantity = item.Quantity,
                Price = item.Price,
                Total = item.Total
            });
        }

        _purchaseRepository
            .AddAsync(purchase)
            .GetAwaiter()
            .GetResult();

        if (RemainingAmount > 0)
        {
            SelectedSupplier.Balance += RemainingAmount;

            _supplierRepository
                .UpdateAsync(SelectedSupplier)
                .GetAwaiter()
                .GetResult();
        }

        foreach (var item in PurchaseItems)
        {
            _inventoryRepository
                .AddAsync(new InventoryTransaction
                {
                    ProductId = item.ProductId,
                    ProductUnitId = item.ProductUnitId,
                    Quantity = item.Quantity,
                    TransactionType = "إدخال",
                    TransactionDate = DateTime.Now,
                    Notes = $"فاتورة شراء رقم {purchase.Id}"
                })
                .GetAwaiter()
                .GetResult();
        }

        PurchaseItems.Clear();

        ProductUnits.Clear();

        SelectedSupplier = null;
        SelectedProduct = null;
        SelectedUnit = null;

        Quantity = 0;
        Price = 0;
        PaidAmount = 0;

        OnPropertyChanged(nameof(GrandTotal));
        OnPropertyChanged(nameof(RemainingAmount));
        OnPropertyChanged(nameof(SelectedSupplier));
        OnPropertyChanged(nameof(SelectedProduct));
        OnPropertyChanged(nameof(SelectedUnit));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(PaidAmount));

        MessageBox.Show("تم حفظ فاتورة الشراء بنجاح.");
    }

    private void RemoveItem(object? parameter)
    {
        if (SelectedPurchaseItem == null)
            return;

        PurchaseItems.Remove(SelectedPurchaseItem);

        OnPropertyChanged(nameof(GrandTotal));
        OnPropertyChanged(nameof(RemainingAmount));
    }
    private SaleItemDto? _selectedPurchaseItem;

    public SaleItemDto? SelectedPurchaseItem
    {
        get => _selectedPurchaseItem;
        set
        {
            SetProperty(ref _selectedPurchaseItem, value);
        }
    }
}