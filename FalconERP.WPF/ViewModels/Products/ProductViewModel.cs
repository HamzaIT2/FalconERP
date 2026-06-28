using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Products;

public class ProductViewModel : BaseViewModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    private string _productName = string.Empty;

    public string ProductName
    {
        get => _productName;
        set
        {
            _productName = value;
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

    private decimal _minimumQuantity;

    public decimal MinimumQuantity
    {
        get => _minimumQuantity;
        set
        {
            _minimumQuantity = value;
            OnPropertyChanged();
        }
    }

    private Category? _selectedCategory;
    private Product? _selectedProduct;

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;

            if (value is not null)
            {
                ProductName = value.Name;
                Quantity = value.Quantity;
                MinimumQuantity = value.MinimumQuantity;

                SelectedCategory = Categories
                    .FirstOrDefault(x => x.Id == value.CategoryId);
            }

            OnPropertyChanged();
        }
    }

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Category> Categories { get; set; }
        = new();

    public ObservableCollection<Product> Products { get; set; }
        = new();



    public ICommand AddProductCommand { get; }

    public ICommand UpdateProductCommand { get; }

    public ICommand DeleteProductCommand { get; }

    public ProductViewModel(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;

        AddProductCommand =
            new RelayCommand(AddProduct);
        UpdateProductCommand =
          new RelayCommand(UpdateProduct);

        DeleteProductCommand =
            new RelayCommand(DeleteProduct);

        LoadCategories();
        LoadProducts();
    }

    private void LoadCategories()
    {
        var categories =
            _categoryRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Categories.Clear();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    private void LoadProducts()
    {
        var products =
            _productRepository
            .GetAllAsync()
            .GetAwaiter()
            .GetResult();

        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }

    private void AddProduct(object? parameter)
    {
        if (SelectedCategory is null)
            return;

        var product = new Product
        {
            Name = ProductName,
            CategoryId = SelectedCategory.Id,
            Quantity = Quantity,
            MinimumQuantity = MinimumQuantity,
            IsActive = true
        };

        _productRepository
            .AddAsync(product)
            .GetAwaiter()
            .GetResult();

        ProductName = string.Empty;
        Quantity = 0;
        MinimumQuantity = 0;

        LoadProducts();
    }
    private void UpdateProduct(object? parameter)
    {
        if (SelectedProduct is null ||
            SelectedCategory is null)
            return;

        SelectedProduct.Name = ProductName;
        SelectedProduct.Quantity = Quantity;
        SelectedProduct.MinimumQuantity = MinimumQuantity;
        SelectedProduct.CategoryId = SelectedCategory.Id;

        _productRepository
            .UpdateAsync(SelectedProduct)
            .GetAwaiter()
            .GetResult();

        LoadProducts();
    }

    private void DeleteProduct(object? parameter)
    {
        if (SelectedProduct is null)
            return;

        _productRepository
            .DeleteAsync(SelectedProduct.Id)
            .GetAwaiter()
            .GetResult();

        ProductName = string.Empty;
        Quantity = 0;
        MinimumQuantity = 0;

        SelectedProduct = null;

        LoadProducts();
    }



}