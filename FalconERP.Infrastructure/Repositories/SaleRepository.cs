using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FalconERP.Application.DTOs;

namespace FalconERP.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(x => x.Items)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);

        await _context.SaveChangesAsync();
    }

    public async Task<List<SaleHistoryDto>> GetHistoryAsync()
    {
        return await _context.Sales
            .Include(x => x.Customer)
            .OrderByDescending(x => x.SaleDate)
            .Select(x => new SaleHistoryDto
            {
                Id = x.Id,

                CustomerName = x.Customer != null
                    ? x.Customer.Name
                    : "نقدي",

                SaleDate = x.SaleDate,

                TotalAmount = x.TotalAmount,

                PaidAmount = x.PaidAmount,

                RemainingAmount = x.RemainingAmount
            })
            .ToListAsync();
    }
    public async Task<SaleDetailsDto?> GetDetailsAsync(int saleId)
    {
        return await _context.Sales

            .Include(x => x.Customer)

            .Include(x => x.Items)
                .ThenInclude(x => x.Product)

            .Include(x => x.Items)
                .ThenInclude(x => x.ProductUnit)

            .Where(x => x.Id == saleId)

            .Select(x => new SaleDetailsDto
            {
                InvoiceNumber = x.Id,

                CustomerName = x.Customer != null
                    ? x.Customer.Name
                    : "نقدي",

                SaleDate = x.SaleDate,

                TotalAmount = x.TotalAmount,

                PaidAmount = x.PaidAmount,

                RemainingAmount = x.RemainingAmount,

                Items = x.Items.Select(i => new SaleItemDetailsDto
                {
                    ProductName = i.Product!.Name,

                    UnitName = i.ProductUnit!.UnitName,

                    Quantity = i.Quantity,

                    Price = i.Price,

                    Total = i.Total

                }).ToList()
            })

            .FirstOrDefaultAsync();
    }


}