using Microsoft.OpenApi.Models;

namespace PatientDiagnostics.Api.Extensions;

/// <summary>
/// Composition-root registrations that belong to the HTTP host, not to Application or Domain.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddControllers();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Patient Diagnostics API",
                Version = "v1",
                Description =
                    "Clinical decision-support API for diabetes risk scoring. Model output is not a diagnosis."
            });

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "PatientDiagnostics.Api.xml");
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        });

        return services;
    }
}
