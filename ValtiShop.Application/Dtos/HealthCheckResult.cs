namespace ValtiShop.Application.Dtos;

public sealed record HealthCheckEntry(
    string Name,
    string Status,
    long DurationMs
);

public sealed record HealthReport(
    string Status,
    DateTime Timestamp,
    IReadOnlyList<HealthCheckEntry>? Checks
);
