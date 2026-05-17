# valti-shop-back-end

C# .NET Core REST API for shop management. DDD with 4 layers.

## Stack

- .NET 10 (`net10.0`) — needs latest SDK
- ASP.NET Core (Presentation), EF Core + SQL Server (Persistence)
- Swagger / Swashbuckle, temporal tables

## Architecture

```
Presentation (Web API) → Application, Persistence
Application → Domain
Persistence → Application, Domain
Domain → (none)
```

- **Domain** entities (`ValtiShop.Domain.Entities/`) are rich: private setters, parameterized constructors, invariant enforcement.
- **Persistence ScaffoldModels** (`ValtiShop.Persistence.ScaffoldModels/`) are EF Core POCOs with public setters — separate from domain entities. The DbContext maps these scaffold models.
- **Application** layer is currently **empty** (no C# files). New use cases go here.
- All tables use **SQL Server temporal tables** (`licenses` schema, `_history` suffix).

## Setup

- **No connection string committed.** Create `ValtiShop.Presentation/appsettings.Development.json` with:
  ```json
  {
    "ConnectionStrings": {
      "ValtiShopDb": "Server=...;Database=ValtiShop;..."
    }
  }
  ```
- No `global.json`, no `Directory.Build.props`, no code analysis config.
- EF Core migrations not yet present — add via Persistence project.

## Commands

```powershell
dotnet build
dotnet run --project ValtiShop.Presentation  # http://localhost:5110
```

## State

- No test projects.
- No CI/CD workflows.
- `ValtiShop.Presentation.http` still has stale `weatherforecast` endpoint from template.
