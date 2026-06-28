using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Categories;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Category;

public partial class CategoryView : UserControl
{
    public CategoryView()
    {
        InitializeComponent();

        var categoryRepository =
            App.Services.GetRequiredService<ICategoryRepository>();

        DataContext =
            new CategoryViewModel(categoryRepository);
    }
}