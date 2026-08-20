# Phase 1 — Solution and Clean Architecture

## Implemented

- .NET 8 solution with six source projects and four test projects
- Clean Architecture project references, enforced by tests that parse csproj files
- Central package management (`Directory.Packages.props`)
- Composition root with Options validation and Swagger
- `IClock` + `SystemClock`
- RFC 7807 `AddProblemDetails()` registration (mapping middleware is Phase 27)
- Placeholder folders for later phases (`ml-data`, `models`, `frontend`, `infra`, `docker`)

## Target framework decision

The development machine has SDK **8.0.100** and runtime **8.0.0**. .NET 10 is not installed. Phase 1 therefore targets `net8.0` and pins Microsoft.Extensions packages to **8.0.0** to match the shared framework.

## Commands

```bash
dotnet restore PatientDiagnostics.sln
dotnet build PatientDiagnostics.sln --configuration Release
dotnet test PatientDiagnostics.sln --configuration Release
dotnet run --project src/PatientDiagnostics.Api --launch-profile https
```

## Tests added

- `LayerDependencyTests` — project and package reference rules
- `DiagnosticsPlatformOptionsTests` — configuration validation
- `DomainExceptionTests` — domain error base type
- `ApiConventionTests` — `/api/v1` routing
- `PlatformInfoTests` — host smoke test and fail-fast options

## Known limitations

- No Patient/Observation entities yet (Phase 2)
- No SQL, Redis, Service Bus, or ML.NET packages yet
- No authentication
- Swagger is enabled in Development and Testing only
- `GET /api/v1/platform/info` is a wiring probe, not a clinical API

## Interview questions for this phase

1. Why does Domain have zero package references?
2. Why does Api not reference Domain directly?
3. Why is ML a separate project instead of a folder in Application?
4. Why parse csproj files for architecture tests instead of `GetReferencedAssemblies()`?
5. Why validate options at startup rather than on first request?
6. Why introduce `IClock` before any clinical entity exists?
