using Microsoft.AspNetCore.Mvc;
using ValtiShop.Application.Interfaces;

namespace ValtiShop.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IHealthCheckService _healthCheckService;

    public HealthController(IHealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet("live")]
    public async Task<IActionResult> GetLiveness()
    {
        var report = await _healthCheckService.CheckLivenessAsync();
        return Ok(report);
    }

    [HttpGet("ready")]
    public async Task<IActionResult> GetReadiness()
    {
        var report = await _healthCheckService.CheckReadinessAsync();
        return report.Status == "Healthy" ? Ok(report) : StatusCode(503, report);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var report = await _healthCheckService.CheckAllAsync();
        return report.Status == "Healthy" ? Ok(report) : StatusCode(503, report);
    }
}
