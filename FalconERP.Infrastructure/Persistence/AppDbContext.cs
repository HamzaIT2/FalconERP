using FalconERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductUnit> ProductUnits => Set<ProductUnit>();


    public DbSet<InventoryTransaction> InventoryTransactions=> Set<InventoryTransaction>();


    public DbSet<Sale> Sales => Set<Sale>();

    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();


    public DbSet<Purchase> Purchases => Set<Purchase>();

    public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();


    public DbSet<SystemSettings> SystemSettings => Set<SystemSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .HasIndex(x => x.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Username)
            .IsUnique();

        modelBuilder.Entity<ProductUnit>()
            .HasOne(x => x.Product)
            .WithMany(x => x.Units)
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);


        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<InventoryTransaction>()
            .HasOne(x => x.ProductUnit)
            .WithMany()
            .HasForeignKey(x => x.ProductUnitId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(x => x.Sale)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.SaleId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(x => x.ProductUnit)
            .WithMany()
            .HasForeignKey(x => x.ProductUnitId);


        modelBuilder.Entity<Sale>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);


        modelBuilder.Entity<PurchaseItem>()
            .HasOne(x => x.Purchase)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.PurchaseId);

        modelBuilder.Entity<PurchaseItem>()
            .HasOne(x => x.Product)
            .WithMany(x => x.PurchaseItems)
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<PurchaseItem>()
            .HasOne(x => x.ProductUnit)
            .WithMany(x => x.PurchaseItems)
            .HasForeignKey(x => x.ProductUnitId);

        modelBuilder.Entity<Purchase>()
            .HasOne(x => x.Supplier)
            .WithMany(x => x.Purchases)
            .HasForeignKey(x => x.SupplierId);




    }
}