using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Persistence.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await GetByIdAsync(id);

        if (supplier == null)
            return;

        _context.Suppliers.Remove(supplier);

        await _context.SaveChangesAsync();
    }
}