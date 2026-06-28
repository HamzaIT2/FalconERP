using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Categories;

public class CategoryViewModel : BaseViewModel
{
    private readonly ICategoryRepository _categoryRepository;

    private string _categoryName = string.Empty;
    private Category? _selectedCategory;

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;

            if (value is not null)
            {
                CategoryName = value.Name;
            }

            OnPropertyChanged();
        }
    }

    public string CategoryName
    {
        get => _categoryName;
        set
        {
            _categoryName = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Category> Categories { get; set; }
        = new();

    public ICommand AddCategoryCommand { get; }
    public ICommand UpdateCategoryCommand { get; }

    public ICommand DeleteCategoryCommand { get; }
    public CategoryViewModel(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        AddCategoryCommand =
            new RelayCommand(AddCategory);

        UpdateCategoryCommand =
            new RelayCommand(UpdateCategory);

        DeleteCategoryCommand =
            new RelayCommand(DeleteCategory);

        LoadCategories();
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

    private void AddCategory(object? parameter)
    {
        if (string.IsNullOrWhiteSpace(CategoryName))
            return;

        var category = new Category
        {
            Name = CategoryName,
            IsActive = true
        };

        _categoryRepository
            .AddAsync(category)
            .GetAwaiter()
            .GetResult();

        CategoryName = string.Empty;

        LoadCategories();
    }
    private void UpdateCategory(object? parameter)
    {
        if (SelectedCategory is null)
            return;

        SelectedCategory.Name = CategoryName;

        _categoryRepository
            .UpdateAsync(SelectedCategory)
            .GetAwaiter()
            .GetResult();

        LoadCategories();
    }
    private void DeleteCategory(object? parameter)
    {
        if (SelectedCategory is null)
            return;

        _categoryRepository
            .DeleteAsync(SelectedCategory.Id)
            .GetAwaiter()
            .GetResult();

        CategoryName = string.Empty;

        SelectedCategory = null;

        LoadCategories();
    }

}