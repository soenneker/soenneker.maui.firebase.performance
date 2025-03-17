using Firebase.Perf.Metrics;
using Soenneker.Maui.Firebase.Performance.Abstract;

namespace Soenneker.Maui.Firebase.Performance.Platforms.Android;

public class FirebasePerformanceTrace : IFirebasePerformanceTrace
{
    private readonly Trace _trace;

    public FirebasePerformanceTrace(Trace trace)
    {
        _trace = trace;
    }

    public void Stop() => _trace.Stop();

    public void LogMetric(string metricName, long value) => _trace.PutMetric(metricName, value);

    public void SetAttribute(string attributeName, string value) => _trace.PutAttribute(attributeName, value);
}