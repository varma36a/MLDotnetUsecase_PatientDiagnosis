using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientDiagnostics.Application.Abstractions.Time;
using PatientDiagnostics.Application.Configuration;
using PatientDiagnostics.Contracts.Common;
using PatientDiagnostics.Contracts.Platform;

namespace PatientDiagnostics.Api.Controllers.V1;

/// <summary>
/// Phase 1 composition-root probe used to verify DI, Options validation, and layer wiring.
/// Clinical endpoints are added in later phases.
/// </summary>
[ApiController]
[Route("api/v1/platform")]
public sealed class PlatformController : ControllerBase
{
    private readonly IOptions<DiagnosticsPlatformOptions> _options;
    private readonly IHostEnvironment _environment;
    private readonly IClock _clock;

    public PlatformController(
        IOptions<DiagnosticsPlatformOptions> options,
        IHostEnvironment environment,
        IClock clock)
    {
        _options = options;
        _environment = environment;
        _clock = clock;
    }

    /// <summary>
    /// Returns non-clinical service metadata. Safe to log; contains no patient data.
    /// </summary>
    [HttpGet("info")]
    [ProducesResponseType(typeof(ServiceInfoResponse), StatusCodes.Status200OK)]
    public ActionResult<ServiceInfoResponse> GetInfo()
    {
        DiagnosticsPlatformOptions options = _options.Value;

        return Ok(new ServiceInfoResponse(
            options.ServiceName,
            _environment.EnvironmentName,
            options.ApiVersion,
            options.DefaultModelName,
            _clock.UtcNow,
            ClinicalDisclaimers.DecisionSupport));
    }
}
