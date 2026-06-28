using FalconERP.Domain.Common;

namespace FalconERP.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public decimal Balance { get; set; }

    public bool IsActive { get; set; } = true;
}