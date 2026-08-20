using System.Reflection;

namespace PatientDiagnostics.Domain;

/// <summary>
/// Assembly marker for the Domain layer.
/// Domain contains enterprise/clinical concepts and has no project dependencies.
/// </summary>
public static class DomainAssembly
{
    public static Assembly Value { get; } = typeof(DomainAssembly).Assembly;
}
