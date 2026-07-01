using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface ISystemSettingsRepository
{
    Task<SystemSettings?> GetAsync();

    Task SaveAsync(SystemSettings settings);
}