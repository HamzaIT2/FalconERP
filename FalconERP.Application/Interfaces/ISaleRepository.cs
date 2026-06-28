using FalconERP.Application.DTOs;
using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface ISaleRepository
{
    Task<List<Sale>> GetAllAsync();

    Task<List<SaleHistoryDto>> GetHistoryAsync();
    Task<SaleDetailsDto?> GetDetailsAsync(int saleId);

    Task AddAsync(Sale sale);
}