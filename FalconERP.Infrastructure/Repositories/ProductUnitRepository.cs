using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Repositories;

public class ProductUnitRepository : IProductUnitRepository
{
    private readonly AppDbContext _context;

    public ProductUnitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductUnit>> GetAllAsync()
    {
        return await _context.ProductUnits
            .Include(x => x.Product)
            .ToListAsync();
    }

    public async Task<ProductUnit?> GetByIdAsync(int id)
    {
        return await _context.ProductUnits
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ProductUnit productUnit)
    {
        await _context.ProductUnits.AddAsync(productUnit);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductUnit productUnit)
    {
        _context.ProductUnits.Update(productUnit);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var unit = await _context.ProductUnits.FindAsync(id);

        if (unit is null)
            return;

        _context.ProductUnits.Remove(unit);

        await _context.SaveChangesAsync();
    }
}