using System.ComponentModel.DataAnnotations;
using PatientDiagnostics.Application.Configuration;

namespace PatientDiagnostics.Application.Tests.Configuration;

public sealed class DiagnosticsPlatformOptionsTests
{
    [Fact]
    public void Default_values_are_valid()
    {
        DiagnosticsPlatformOptions options = new();

        bool isValid = TryValidate(options, out List<ValidationResult> results);

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ServiceName_must_not_be_missing_or_whitespace(string? serviceName)
    {
        DiagnosticsPlatformOptions options = new() { ServiceName = serviceName! };

        bool isValid = TryValidate(options, out List<ValidationResult> results);

        isValid.Should().BeFalse();
        results.Should().Contain(result => result.MemberNames.Contains(nameof(DiagnosticsPlatformOptions.ServiceName)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefaultModelName_must_not_be_missing_or_whitespace(string? modelName)
    {
        DiagnosticsPlatformOptions options = new() { DefaultModelName = modelName! };

        bool isValid = TryValidate(options, out List<ValidationResult> results);

        isValid.Should().BeFalse();
        results.Should().Contain(result => result.MemberNames.Contains(nameof(DiagnosticsPlatformOptions.DefaultModelName)));
    }

    private static bool TryValidate(DiagnosticsPlatformOptions options, out List<ValidationResult> results)
    {
        ValidationContext context = new(options);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(options, context, results, validateAllProperties: true);
    }
}
