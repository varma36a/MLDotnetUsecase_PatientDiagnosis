using PatientDiagnostics.Application.Abstractions.Time;

namespace PatientDiagnostics.Application.Tests.Abstractions;

public sealed class FakeClock : IClock
{
    public FakeClock(DateTimeOffset utcNow)
    {
        UtcNow = utcNow;
    }

    public DateTimeOffset UtcNow { get; set; }
}

public sealed class FakeClockTests
{
    [Fact]
    public void Returns_the_configured_timestamp()
    {
        DateTimeOffset expected = new(2026, 8, 20, 13, 30, 0, TimeSpan.Zero);
        FakeClock clock = new(expected);

        clock.UtcNow.Should().Be(expected);
    }
}
