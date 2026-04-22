using Soenneker.Tests.HostedUnit;

namespace Soenneker.Maui.Firebase.Performance.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class FirebasePerformanceServiceTests : HostedUnitTest
{


    public FirebasePerformanceServiceTests(Host host) : base(host)
    {

    }

    [Test]
    public void Default()
    {

    }
}
