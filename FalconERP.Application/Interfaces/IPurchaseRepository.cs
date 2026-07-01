using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface IPurchaseRepository
{
    Task<List<Purchase>> GetAllAsync();

    Task AddAsync(Purchase purchase);
}