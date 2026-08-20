# PatientDiagnostics

Clinical decision-support platform for **diabetes risk scoring**. This is not an autonomous diagnostic system. ML.NET returns a risk score and supporting information; the final diagnosis remains with a qualified clinician.

**Current status:** Phase 1 complete — solution, Clean Architecture, composition root, and architecture tests.

Inspected SDK on this machine: **.NET 8.0.100**. The solution targets `net8.0`. .NET 10 is not installed here, so it is not used.

## Architecture

```text
Angular UI (Phase 12)
        │
        ▼
PatientDiagnostics.Api          HTTP adapters, auth, Swagger, composition root
        │
        ├── PatientDiagnostics.Application    use cases, validators, abstractions
        │         │
        │         └── PatientDiagnostics.Domain    entities, value objects, domain errors
        │
        ├── PatientDiagnostics.Infrastructure     EF Core, Redis, Service Bus, Blob, FHIR
        ├── PatientDiagnostics.ML                 training, features, evaluation, prediction
        └── PatientDiagnostics.Contracts          versioned HTTP contracts
```

Dependency rule: inner layers never reference outer layers.

| Project | May reference | Must not reference |
| --- | --- | --- |
| Domain | nothing | Application, Infrastructure, ML, Api, Contracts |
| Contracts | nothing | Domain, Application, Infrastructure |
| Application | Domain | Infrastructure, ML, Api |
| Infrastructure | Application, Domain | Api, ML |
| ML | Application, Domain | Api, Infrastructure, FHIR types |
| Api | Application, Infrastructure, ML, Contracts | Domain entities |

The API is the composition root. It wires implementations; it does not contain business or ML logic.

## Why this split (interview)

- **Clean Architecture** keeps clinical rules independent of ASP.NET, EF Core, and Azure. Those adapters can change without rewriting the decision-support workflow.
- **SQL** is the system of record for patients, observations, and audit. Redis is a cache, not a source of truth.
- **ML.NET** stays in its own project so LightGBM native assets and training pipelines cannot leak into Domain or FHIR mapping.
- **Contracts** are HTTP DTOs. Domain entities are not serialized to clients.
- **Synchronous prediction** will be used for a single clinician request. Service Bus is for audit, notifications, and batch work that can complete after the HTTP response.
- **LLM and ML stay separate.** An LLM may extract structured observations from notes. It must never emit a diagnosis or replace the LightGBM risk model.

## Project structure

```text
PatientDiagnostics.sln
src/
  PatientDiagnostics.Api/
  PatientDiagnostics.Application/
  PatientDiagnostics.Domain/
  PatientDiagnostics.Infrastructure/
  PatientDiagnostics.ML/
  PatientDiagnostics.Contracts/
tests/
  PatientDiagnostics.Api.Tests/
  PatientDiagnostics.Application.Tests/
  PatientDiagnostics.ML.Tests/
  PatientDiagnostics.Integration.Tests/
ml-data/ raw/ processed/ training/ test/
models/
frontend/patient-diagnostics-ui/
infra/bicep/
docker/
docs/
```

## Prerequisites

- .NET SDK 8.0.100 or later 8.0.x (`dotnet --list-sdks`)
- SQL Server / Azure SQL, Redis, Azure Service Bus — **not required for Phase 1**

## Local development

```bash
dotnet restore PatientDiagnostics.sln
dotnet build PatientDiagnostics.sln --configuration Release
dotnet test PatientDiagnostics.sln --configuration Release
dotnet run --project src/PatientDiagnostics.Api
```

Then open `https://localhost:7080/swagger` or `GET /api/v1/platform/info`.

That endpoint is a composition-root probe. Clinical routes start in later phases.

## Configuration

`DiagnosticsPlatform` is validated at startup (`ValidateOnStart`). Missing `ServiceName` fails the host instead of a later request.

Secrets stay out of source control. Use user secrets, environment variables, or Key Vault (Phase 16). The API project already has a `UserSecretsId`.

## Phase roadmap

| Phase | Scope | Status |
| --- | --- | --- |
| 1 | Solution + Clean Architecture | **Done** |
| 2 | Domain + EF Core + SQL | Not started |
| 3 | Patient + Observation APIs | Not started |
| 4–8 | Dataset, ML.NET, evaluation, prediction, diagnostic API | Not started |
| 9–11 | Outbox, Service Bus, Redis, FHIR | Not started |
| 12–14 | Angular, security, observability | Not started |
| 15–17 | Docker, Bicep, Azure DevOps | Not started |
| 18–20 | Model versioning, drift, full test suite | Not started |

## Testing

Phase 1 tests cover:

- Clean Architecture project-reference rules
- Options validation
- Domain exception base type
- API v1 routing conventions
- Host startup through `WebApplicationFactory`
- Fail-fast when required configuration is invalid

## Documentation

- [Clean Architecture](docs/architecture.md)
- [Phase 1 notes](docs/phase-01.md)

Later README sections (database, ML training, Docker, Azure, FHIR, drift) will be added when those phases land. Do not infer production clinical performance from this repository; training data will be synthetic.
