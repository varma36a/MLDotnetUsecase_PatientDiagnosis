using PatientDiagnostics.Domain.Exceptions;

namespace PatientDiagnostics.Application.Tests.Domain;

public sealed class DomainExceptionTests
{
    [Fact]
    public void Carries_the_business_rule_message()
    {
        TestDomainException exception = new("BMI is outside the supported clinical range.");

        exception.Message.Should().Be("BMI is outside the supported clinical range.");
        exception.Should().BeAssignableTo<DomainException>();
    }

    private sealed class TestDomainException : DomainException
    {
        public TestDomainException(string message)
            : base(message)
        {
        }
    }
}
