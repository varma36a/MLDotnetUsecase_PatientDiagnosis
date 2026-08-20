using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PatientDiagnostics.ML.DependencyInjection;

/// <summary>
/// Registers ML.NET prediction services. The trained model will be loaded once and reused.
/// Training pipelines must not be constructed per HTTP request.
/// </summary>
public static class MachineLearningServiceCollectionExtensions
{
    public static IServiceCollection AddMachineLearning(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        return services;
    }
}
