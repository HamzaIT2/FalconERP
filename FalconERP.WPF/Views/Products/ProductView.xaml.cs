using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Products;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Products;

public partial class ProductView : UserControl
{
    public ProductView()
    {
        InitializeComponent();

        var productRepository =
            App.Services.GetRequiredService<IProductRepository>();

        var categoryRepository =
            App.Services.GetRequiredService<ICategoryRepository>();

        DataContext = new ProductViewModel(
            productRepository,
            categoryRepository);
    }
}