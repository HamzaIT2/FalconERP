using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int RoleId { get; set; }

    public Role? Role { get; set; }
}