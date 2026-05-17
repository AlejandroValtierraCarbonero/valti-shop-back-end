using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ValtiShop.Application.Dtos;
using ValtiShop.Application.Interfaces;
using ValtiShop.Persistence.Data;

namespace ValtiShop.Persistence.Services;

public sealed class HealthCheckService : IHealthCheckService
{
    private readonly ValtiShopDbContext _context;

    public HealthCheckService(ValtiShopDbContext context)
    {
        _context = context;
    }

    public Task<HealthReport> CheckLivenessAsync()
    {
        return Task.FromResult(new HealthReport("Healthy", DateTime.UtcNow, null));
    }

    public async Task<HealthReport> CheckReadinessAsync()
    {
        var entry = await CheckDatabaseAsync();
        var status = entry.Status == "Healthy" ? "Healthy" : "Unhealthy";
        return new HealthReport(status, DateTime.UtcNow, [entry]);
    }

    public async Task<HealthReport> CheckAllAsync()
    {
        var dbEntry = await CheckDatabaseAsync();
        var status = dbEntry.Status == "Healthy" ? "Healthy" : "Unhealthy";
        return new HealthReport(
            status,
            DateTime.UtcNow,
            [
                new HealthCheckEntry("Liveness", "Healthy", 0),
                dbEntry
            ]);
    }

    private async Task<HealthCheckEntry> CheckDatabaseAsync()
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _context.Database.CanConnectAsync();
            sw.Stop();
            return new HealthCheckEntry("Database", "Healthy", sw.ElapsedMilliseconds);
        }
        catch
        {
            sw.Stop();
            return new HealthCheckEntry("Database", "Unhealthy", sw.ElapsedMilliseconds);
        }
    }
}
