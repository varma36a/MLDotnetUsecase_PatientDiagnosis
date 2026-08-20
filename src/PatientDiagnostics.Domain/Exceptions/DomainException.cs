namespace PatientDiagnostics.Domain.Exceptions;

/// <summary>
/// Base type for business-rule violations.
/// Application and API layers can map this to RFC 7807 ProblemDetails without leaking infrastructure types into Domain.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
