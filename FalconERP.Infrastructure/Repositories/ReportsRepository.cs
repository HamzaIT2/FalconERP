using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly AppDbContext _context;

    public ReportsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sale>> GetSalesReportAsync()
    {
        return await _context.Sales
            .Include(x => x.Customer)
            .Include(x => x.Items)
            .OrderByDescending(x => x.SaleDate)
            .ToListAsync();
    }
}