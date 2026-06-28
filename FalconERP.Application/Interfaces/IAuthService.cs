using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(string username, string password);

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}