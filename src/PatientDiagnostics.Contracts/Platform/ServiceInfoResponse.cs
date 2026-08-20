namespace PatientDiagnostics.Contracts.Platform;

/// <summary>
/// Phase 1 composition-root probe. Not a clinical endpoint.
/// </summary>
public sealed record ServiceInfoResponse(
    string ServiceName,
    string Environment,
    string ApiVersion,
    string DefaultModelName,
    DateTimeOffset ServerTimeUtc,
    string ClinicalDisclaimer);
