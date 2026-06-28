using BCrypt.Net;
using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FalconERP.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Username == username);

        if (user is null)
            return null;

        if (!VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }
}