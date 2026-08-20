using PatientDiagnostics.Api.Extensions;
using PatientDiagnostics.Application.DependencyInjection;
using PatientDiagnostics.Infrastructure.DependencyInjection;
using PatientDiagnostics.ML.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMachineLearning(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.UseApiPipeline();
app.Run();

/// <summary>
/// Exposed for <c>WebApplicationFactory</c> in test projects.
/// </summary>
public partial class Program;
