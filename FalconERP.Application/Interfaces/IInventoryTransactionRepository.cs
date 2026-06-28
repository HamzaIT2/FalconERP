using FalconERP.Application.DTOs;
using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface IInventoryTransactionRepository
{
    Task<List<InventoryTransaction>> GetAllAsync();

    Task AddAsync(InventoryTransaction transaction);

    Task<List<StockBalanceDto>> GetStockBalancesAsync();

}
