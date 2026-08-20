using System.Xml.Linq;

namespace PatientDiagnostics.Application.Tests.Architecture;

/// <summary>
/// Enforces Clean Architecture project references by reading csproj files.
/// Assembly.GetReferencedAssemblies() is not sufficient because the compiler omits unused references.
/// </summary>
public sealed class LayerDependencyTests
{
    private static readonly string RepoRoot = FindRepoRoot();

    [Fact]
    public void Domain_should_not_reference_other_solution_projects()
    {
        GetProjectReferenceNames("src/PatientDiagnostics.Domain/PatientDiagnostics.Domain.csproj")
            .Should()
            .BeEmpty("Domain is the innermost layer and must not depend on Application, Infrastructure, ML, Contracts, or Api");
    }

    [Fact]
    public void Contracts_should_not_reference_other_solution_projects()
    {
        GetProjectReferenceNames("src/PatientDiagnostics.Contracts/PatientDiagnostics.Contracts.csproj")
            .Should()
            .BeEmpty("HTTP contracts must stay independent of domain and infrastructure types");
    }

    [Fact]
    public void Application_should_only_reference_domain()
    {
        GetProjectReferenceNames("src/PatientDiagnostics.Application/PatientDiagnostics.Application.csproj")
            .Should()
            .Equal("PatientDiagnostics.Domain");
    }

    [Fact]
    public void Infrastructure_should_only_reference_application_and_domain()
    {
        GetProjectReferenceNames("src/PatientDiagnostics.Infrastructure/PatientDiagnostics.Infrastructure.csproj")
            .Should()
            .BeEquivalentTo("PatientDiagnostics.Application", "PatientDiagnostics.Domain");
    }

    [Fact]
    public void Ml_should_only_reference_application_and_domain()
    {
        GetProjectReferenceNames("src/PatientDiagnostics.ML/PatientDiagnostics.ML.csproj")
            .Should()
            .BeEquivalentTo("PatientDiagnostics.Application", "PatientDiagnostics.Domain");
    }

    [Fact]
    public void Api_should_not_reference_domain_directly()
    {
        IReadOnlyCollection<string> references =
            GetProjectReferenceNames("src/PatientDiagnostics.Api/PatientDiagnostics.Api.csproj");

        references.Should().BeEquivalentTo(
            "PatientDiagnostics.Application",
            "PatientDiagnostics.Contracts",
            "PatientDiagnostics.Infrastructure",
            "PatientDiagnostics.ML");

        references.Should().NotContain("PatientDiagnostics.Domain");
    }

    [Fact]
    public void Domain_should_not_have_package_references()
    {
        GetPackageReferenceNames("src/PatientDiagnostics.Domain/PatientDiagnostics.Domain.csproj")
            .Should()
            .BeEmpty("Domain must remain free of framework and vendor packages");
    }

    private static IReadOnlyCollection<string> GetProjectReferenceNames(string relativeCsprojPath)
    {
        XDocument document = XDocument.Load(Path.Combine(RepoRoot, relativeCsprojPath));

        return document
            .Descendants("ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFileNameWithoutExtension(include!.Replace('\\', '/')))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
    }

    private static IReadOnlyCollection<string> GetPackageReferenceNames(string relativeCsprojPath)
    {
        XDocument document = XDocument.Load(Path.Combine(RepoRoot, relativeCsprojPath));

        return document
            .Descendants("PackageReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Select(name => name!)
            .ToArray();
    }

    private static string FindRepoRoot()
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "PatientDiagnostics.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not locate PatientDiagnostics.sln from the test output directory.");
    }
}
