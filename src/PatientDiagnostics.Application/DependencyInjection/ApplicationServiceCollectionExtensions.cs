using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientDiagnostics.Application.Configuration;

namespace PatientDiagnostics.Application.DependencyInjection;

/// <summary>
/// Registers Application-layer services. Use cases will be added here in later phases.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions<DiagnosticsPlatformOptions>()
            .Bind(configuration.GetSection(DiagnosticsPlatformOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
