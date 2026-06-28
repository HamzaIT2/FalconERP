using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FalconERP.Application.DTOs;
namespace FalconERP.Infrastructure.Repositories;

public class InventoryTransactionRepository: IInventoryTransactionRepository
{
    private readonly AppDbContext _context;

    public InventoryTransactionRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryTransaction>> GetAllAsync()
    {
        return await _context.InventoryTransactions
            .Include(x => x.Product)
            .Include(x => x.ProductUnit)
            .ToListAsync();
    }

    public async Task AddAsync(
        InventoryTransaction transaction)
    {
        await _context.InventoryTransactions
            .AddAsync(transaction);

        await _context.SaveChangesAsync();
    }
    public async Task<List<StockBalanceDto>> GetStockBalancesAsync()
    {
        var transactions = await _context.InventoryTransactions
            .Include(x => x.Product)
            .ToListAsync();

        var result = transactions
            .GroupBy(x => x.Product!.Name)
            .Select(g => new StockBalanceDto
            {
                ProductName = g.Key,

                TotalIn = g
                    .Where(x => x.TransactionType == "إدخال")
                    .Sum(x => x.Quantity),

                TotalOut = g
                    .Where(x => x.TransactionType == "إخراج")
                    .Sum(x => x.Quantity),

                Balance =
                    g.Where(x => x.TransactionType == "إدخال")
                     .Sum(x => x.Quantity)
                    -
                    g.Where(x => x.TransactionType == "إخراج")
                     .Sum(x => x.Quantity)
            })
            .ToList();

        return result;
    }

}