using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface IProductUnitRepository
{
    Task<List<ProductUnit>> GetAllAsync();

    Task<ProductUnit?> GetByIdAsync(int id);

    Task AddAsync(ProductUnit productUnit);

    Task UpdateAsync(ProductUnit productUnit);

    Task DeleteAsync(int id);
}