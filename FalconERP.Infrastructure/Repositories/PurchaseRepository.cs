using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly AppDbContext _context;

    public PurchaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Purchase>> GetAllAsync()
    {
        return await _context.Purchases
            .Include(x => x.Supplier)
            .Include(x => x.Items)
            .ToListAsync();
    }

    public async Task AddAsync(Purchase purchase)
    {
        _context.Purchases.Add(purchase);

        await _context.SaveChangesAsync();
    }
}