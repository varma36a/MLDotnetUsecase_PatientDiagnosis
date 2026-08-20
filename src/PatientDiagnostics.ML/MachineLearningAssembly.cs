using System.Reflection;

namespace PatientDiagnostics.ML;

/// <summary>
/// Assembly marker for ML.NET training, feature engineering, evaluation, and prediction.
/// Isolated so LightGBM native dependencies cannot leak into Domain or FHIR mapping.
/// </summary>
public static class MachineLearningAssembly
{
    public static Assembly Value { get; } = typeof(MachineLearningAssembly).Assembly;
}
