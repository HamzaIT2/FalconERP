using FalconERP.Application.Interfaces;
using FalconERP.Infrastructure.Persistence;
using FalconERP.Infrastructure.Persistence.Repositories;
using FalconERP.Infrastructure.Printing;
using FalconERP.Infrastructure.Repositories;
using FalconERP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FalconERP.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=falcon.db"));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductUnitRepository, ProductUnitRepository>();
        services.AddScoped<IInventoryTransactionRepository,InventoryTransactionRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IExcelExportService, ExcelExportService>();
        services.AddScoped<ISystemSettingsRepository,SystemSettingsRepository>();
        services.AddScoped<ReceiptFactory>();

        services.AddScoped<ReceiptPrinterService>();
        return services;
    }
}