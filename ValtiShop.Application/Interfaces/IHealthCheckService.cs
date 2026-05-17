using ValtiShop.Application.Dtos;

namespace ValtiShop.Application.Interfaces;

public interface IHealthCheckService
{
    Task<HealthReport> CheckLivenessAsync();
    Task<HealthReport> CheckReadinessAsync();
    Task<HealthReport> CheckAllAsync();
}
