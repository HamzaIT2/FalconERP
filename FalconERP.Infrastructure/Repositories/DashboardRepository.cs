using FalconERP.Application.Interfaces;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;

    public DashboardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetProductsCountAsync()
        => await _context.Products.CountAsync();

    public async Task<int> GetCustomersCountAsync()
        => await _context.Customers.CountAsync();

    public async Task<int> GetSuppliersCountAsync()
        => await _context.Suppliers.CountAsync();

    public async Task<decimal> GetTodaySalesAsync()
    {
        var today = DateTime.Today;

        return await _context.Sales
            .Where(x => x.SaleDate.Date == today)
            .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;
    }

    public async Task<decimal> GetTodayPurchasesAsync()
    {
        var today = DateTime.Today;

        return await _context.Purchases
            .Where(x => x.PurchaseDate.Date == today)
            .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;
    }

    public async Task<int> GetLowStockProductsAsync()
    {
        return await _context.Products
            .CountAsync(x => x.Quantity <= x.MinimumQuantity);
    }
}