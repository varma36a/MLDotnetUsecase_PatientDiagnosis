using System.Reflection;

namespace PatientDiagnostics.Contracts;

/// <summary>
/// Assembly marker for versioned public API contracts.
/// These types are serialization-friendly and must not contain domain behavior.
/// </summary>
public static class ContractsAssembly
{
    public static Assembly Value { get; } = typeof(ContractsAssembly).Assembly;
}
