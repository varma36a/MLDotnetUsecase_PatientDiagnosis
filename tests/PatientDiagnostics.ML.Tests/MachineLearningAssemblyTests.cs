using PatientDiagnostics.ML;

namespace PatientDiagnostics.ML.Tests;

public sealed class MachineLearningAssemblyTests
{
    [Fact]
    public void Assembly_is_loaded_and_isolated_from_the_api_host()
    {
        MachineLearningAssembly.Value.GetName().Name.Should().Be("PatientDiagnostics.ML");
    }
}
