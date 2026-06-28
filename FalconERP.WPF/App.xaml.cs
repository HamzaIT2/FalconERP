using FalconERP.Infrastructure.DependencyInjection;
using FalconERP.Infrastructure.Persistence;
using FalconERP.Infrastructure.Persistence.Seed;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore; 
namespace FalconERP.WPF;

public partial class App : System.Windows.Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        
        try
        {
            var services = new ServiceCollection();

            services.AddInfrastructure();

            Services = services.BuildServiceProvider();

            using var scope = Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            DatabaseSeeder.SeedAsync(dbContext).Wait();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        base.OnStartup(e);
    }
}