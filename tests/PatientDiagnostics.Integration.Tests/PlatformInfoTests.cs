using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PatientDiagnostics.Contracts.Common;
using PatientDiagnostics.Contracts.Platform;

namespace PatientDiagnostics.Integration.Tests;

public sealed class PlatformInfoTests : IClassFixture<PatientDiagnosticsApiFactory>
{
    private readonly HttpClient _client;

    public PlatformInfoTests(PatientDiagnosticsApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_platform_info_returns_non_clinical_service_metadata()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/platform/info");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        ServiceInfoResponse? payload = await response.Content.ReadFromJsonAsync<ServiceInfoResponse>();
        payload.Should().NotBeNull();
        payload!.ServiceName.Should().Be("PatientDiagnostics.Testing");
        payload.Environment.Should().Be("Testing");
        payload.ApiVersion.Should().Be("v1");
        payload.DefaultModelName.Should().Be("diabetes-risk");
        payload.ClinicalDisclaimer.Should().Be(ClinicalDisclaimers.DecisionSupport);
        payload.ServerTimeUtc.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Host_should_fail_fast_when_required_options_are_missing()
    {
        using WebApplicationFactory<Program> factory = new PatientDiagnosticsApiFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["DiagnosticsPlatform:ServiceName"] = " "
                    });
                });
            });

        Action act = () => factory.CreateClient();

        act.Should().Throw<OptionsValidationException>();
    }
}
