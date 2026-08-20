using System.Reflection;

namespace PatientDiagnostics.Application;

/// <summary>
/// Assembly marker for the Application layer (use cases, DTOs, validators, and abstractions).
/// </summary>
public static class ApplicationAssembly
{
    public static Assembly Value { get; } = typeof(ApplicationAssembly).Assembly;
}
