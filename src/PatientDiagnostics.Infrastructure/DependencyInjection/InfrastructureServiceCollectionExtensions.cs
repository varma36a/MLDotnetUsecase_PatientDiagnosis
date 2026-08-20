using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientDiagnostics.Application.Abstractions.Time;
using PatientDiagnostics.Infrastructure.Time;

namespace PatientDiagnostics.Infrastructure.DependencyInjection;

/// <summary>
/// Registers Infrastructure adapters. EF Core, Redis, Service Bus, and Blob Storage are added in later phases.
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<IClock, SystemClock>();

        return services;
    }
}
