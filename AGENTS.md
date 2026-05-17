# valti-shop-back-end

C# .NET 10 REST API for shop management. DDD with 4 layers.

## Stack

- ASP.NET Core, EF Core + SQL Server, Swagger/Swashbuckle
- Temporal tables (`licenses` schema, `_history` suffix)

## Architecture

```
Presentation → Application, Persistence
Application → Domain
Persistence → Application, Domain
Domain → (none)
```

- **Domain** (`ValtiShop.Domain.Entities/`): rich entities — private setters, parameterized constructors, invariant enforcement.
- **ScaffoldModels** (`ValtiShop.Persistence.ScaffoldModels/`): EF Core POCOs with public setters, separate from domain entities. `ValtiShopDbContext` maps scaffold models, not domain entities.
- **Application** (`ValtiShop.Application/`): currently thin — `Interfaces/` and `Dtos/` only, no use-case services yet.
- `ValtiShopDbContext` is `partial`; new entity config goes in `OnModelCreatingPartial`.
- Project references in `.csproj` files match the dependency arrows above exactly.

## Adding new entities

1. Add rich domain entity in `ValtiShop.Domain.Entities/`
2. Add EF Core scaffold POCO in `ValtiShop.Persistence.ScaffoldModels/`
3. Add `DbSet<T>` + Fluent config in `ValtiShopDbContext` (via `OnModelCreatingPartial`)
4. Create migration from Persistence: `dotnet ef migrations add <name> --project ValtiShop.Persistence`

## Conventions

- File-scoped namespaces (`namespace X.Y;`)
- Service implementations are `sealed`; DTOs use `sealed record`
- Interfaces named `I*Service` in `ValtiShop.Application.Interfaces/`

## Setup

- **No connection string committed.** Create `ValtiShop.Presentation/appsettings.Development.json`:
  ```json
  {
    "ConnectionStrings": {
      "ValtiShopDb": "Server=...;Database=ValtiShop;..."
    }
  }
  ```
- No `global.json`, no `Directory.Build.props`.
- EF Core migrations not yet present — create via Persistence project.
- `UserSecretsId` configured in Presentation project (`dotnet user-secrets` available).

## Commands

```
dotnet build
dotnet run --project ValtiShop.Presentation  # http://localhost:5110
```

## State

- No test projects, no CI/CD workflows.
- `ValtiShop.Presentation.http` has stale `weatherforecast` endpoint from template.
