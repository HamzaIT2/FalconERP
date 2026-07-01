using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Repositories;

public class SystemSettingsRepository
    : ISystemSettingsRepository
{
    private readonly AppDbContext _context;

    public SystemSettingsRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemSettings?> GetAsync()
    {
        return await _context.SystemSettings
            .FirstOrDefaultAsync();
    }

    public async Task SaveAsync(
        SystemSettings settings)
    {
        var old = await _context.SystemSettings
            .FirstOrDefaultAsync();

        if (old == null)
        {
            _context.SystemSettings.Add(settings);
        }
        else
        {
            old.StoreName = settings.StoreName;
            old.OwnerName = settings.OwnerName;
            old.Phone = settings.Phone;
            old.Email = settings.Email;
            old.Address = settings.Address;
            old.Currency = settings.Currency;
            old.PrinterType = settings.PrinterType;
            old.ReceiptFooter = settings.ReceiptFooter;
            old.LogoPath = settings.LogoPath;
        }

        await _context.SaveChangesAsync();
    }
}