using PatientDiagnostics.Application.Abstractions.Time;

namespace PatientDiagnostics.Infrastructure.Time;

/// <summary>
/// Production clock. Registered as a singleton because it is stateless.
/// </summary>
public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
