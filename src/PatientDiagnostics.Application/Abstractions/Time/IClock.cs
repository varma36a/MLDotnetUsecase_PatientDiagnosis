namespace PatientDiagnostics.Application.Abstractions.Time;

/// <summary>
/// Abstraction over the system clock so audit timestamps and tests are deterministic.
/// Do not call <see cref="DateTimeOffset.UtcNow"/> from application services.
/// </summary>
public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
