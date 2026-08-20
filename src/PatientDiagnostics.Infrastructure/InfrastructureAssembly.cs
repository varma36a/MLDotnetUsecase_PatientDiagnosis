using System.Reflection;

namespace PatientDiagnostics.Infrastructure;

/// <summary>
/// Assembly marker for Infrastructure adapters (EF Core, Redis, Service Bus, Blob, FHIR).
/// </summary>
public static class InfrastructureAssembly
{
    public static Assembly Value { get; } = typeof(InfrastructureAssembly).Assembly;
}
