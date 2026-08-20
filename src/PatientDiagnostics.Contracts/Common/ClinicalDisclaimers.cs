namespace PatientDiagnostics.Contracts.Common;

/// <summary>
/// Standard clinical-safety wording for decision-support responses.
/// The platform must never present a model output as a diagnosis.
/// </summary>
public static class ClinicalDisclaimers
{
    public const string DecisionSupport =
        "This result is clinical decision support and is not a diagnosis. The final diagnosis must remain with a qualified clinician.";
}
