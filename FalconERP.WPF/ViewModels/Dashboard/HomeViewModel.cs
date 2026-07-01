using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Shared;

namespace FalconERP.WPF.ViewModels.Dashboard;

public class HomeViewModel : BaseViewModel
{
    private readonly IDashboardRepository _dashboardRepository;

    private int _productsCount;
    public int ProductsCount
    {
        get => _productsCount;
        set => SetProperty(ref _productsCount, value);
    }

    private int _customersCount;
    public int CustomersCount
    {
        get => _customersCount;
        set => SetProperty(ref _customersCount, value);
    }

    private int _suppliersCount;
    public int SuppliersCount
    {
        get => _suppliersCount;
        set => SetProperty(ref _suppliersCount, value);
    }

    private decimal _todaySales;
    public decimal TodaySales
    {
        get => _todaySales;
        set => SetProperty(ref _todaySales, value);
    }

    private decimal _todayPurchases;
    public decimal TodayPurchases
    {
        get => _todayPurchases;
        set => SetProperty(ref _todayPurchases, value);
    }

    private int _lowStockProducts;
    public int LowStockProducts
    {
        get => _lowStockProducts;
        set => SetProperty(ref _lowStockProducts, value);
    }

    public HomeViewModel(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;

        LoadDashboard();
    }

    private void LoadDashboard()
    {
        ProductsCount = _dashboardRepository.GetProductsCountAsync().Result;
        CustomersCount = _dashboardRepository.GetCustomersCountAsync().Result;
        SuppliersCount = _dashboardRepository.GetSuppliersCountAsync().Result;
        TodaySales = _dashboardRepository.GetTodaySalesAsync().Result;
        TodayPurchases = _dashboardRepository.GetTodayPurchasesAsync().Result;
        LowStockProducts = _dashboardRepository.GetLowStockProductsAsync().Result;
    }
}