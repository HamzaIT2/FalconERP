namespace FalconERP.Application.Interfaces;

public interface IDashboardRepository
{
    Task<int> GetProductsCountAsync();

    Task<int> GetCustomersCountAsync();

    Task<int> GetSuppliersCountAsync();

    Task<decimal> GetTodaySalesAsync();

    Task<decimal> GetTodayPurchasesAsync();

    Task<int> GetLowStockProductsAsync();
}