using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PatientDiagnostics.Api.Controllers.V1;

namespace PatientDiagnostics.Api.Tests.Controllers;

public sealed class ApiConventionTests
{
    [Fact]
    public void Versioned_controllers_should_use_the_api_v1_route_prefix()
    {
        IEnumerable<Type> controllers = typeof(PlatformController).Assembly
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(ControllerBase)) && !type.IsAbstract);

        controllers.Should().NotBeEmpty();

        foreach (Type controller in controllers)
        {
            RouteAttribute? route = controller.GetCustomAttribute<RouteAttribute>();
            route.Should().NotBeNull($"controller {controller.Name} must declare a route");
            route!.Template.Should().StartWith("api/v1", $"controller {controller.Name} must be versioned");
        }
    }

    [Fact]
    public void Controllers_should_live_in_the_v1_namespace_until_v2_is_introduced()
    {
        IEnumerable<Type> controllers = typeof(PlatformController).Assembly
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(ControllerBase)) && !type.IsAbstract);

        controllers.Should().OnlyContain(type =>
            type.Namespace == "PatientDiagnostics.Api.Controllers.V1");
    }
}
