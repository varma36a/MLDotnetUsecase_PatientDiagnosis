namespace PatientDiagnostics.Domain.Primitives;

/// <summary>
/// Marks an entity as an aggregate root.
/// Persistence and application services should load and save aggregates through this boundary,
/// not through child entities.
/// </summary>
public interface IAggregateRoot
{
}
