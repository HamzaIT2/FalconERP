using FalconERP.Domain.Entities;

namespace FalconERP.Application.Interfaces;

public interface IReportsRepository
{
    Task<List<Sale>> GetSalesReportAsync();
}