using System.ComponentModel.DataAnnotations;

namespace PatientDiagnostics.Application.Configuration;

/// <summary>
/// Validated platform options. Bound at startup so misconfiguration fails the host instead of a later request.
/// </summary>
public sealed class DiagnosticsPlatformOptions : IValidatableObject
{
    public const string SectionName = "DiagnosticsPlatform";

    [Required]
    public string ServiceName { get; set; } = "PatientDiagnostics";

    [Required]
    public string DefaultModelName { get; set; } = "diabetes-risk";

    [Required]
    public string ApiVersion { get; set; } = "v1";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ServiceName))
        {
            yield return new ValidationResult("ServiceName must be provided.", new[] { nameof(ServiceName) });
        }

        if (string.IsNullOrWhiteSpace(DefaultModelName))
        {
            yield return new ValidationResult("DefaultModelName must be provided.", new[] { nameof(DefaultModelName) });
        }

        if (string.IsNullOrWhiteSpace(ApiVersion))
        {
            yield return new ValidationResult("ApiVersion must be provided.", new[] { nameof(ApiVersion) });
        }
    }
}
