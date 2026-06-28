using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Products;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace FalconERP.WPF.Views.Products;

public partial class ProductUnitView : UserControl
{
    public ProductUnitView()
    {
        InitializeComponent();

        var productUnitRepository =
            App.Services.GetRequiredService<IProductUnitRepository>();

        var productRepository =
            App.Services.GetRequiredService<IProductRepository>();

        DataContext = new ProductUnitViewModel(
            productUnitRepository,
            productRepository);
    }
}