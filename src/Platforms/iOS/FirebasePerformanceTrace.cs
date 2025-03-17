using Firebase.PerformanceMonitoring;
using Soenneker.Maui.Firebase.Performance.Abstract;

namespace Soenneker.Maui.Firebase.Performance.Platforms.iOS;

/// <summary>
/// Private class that wraps Firebase's Trace implementation.
/// </summary>
public class FirebasePerformanceTrace : IFirebasePerformanceTrace
{
    private readonly Trace _trace;

    public FirebasePerformanceTrace(Trace trace)
    {
        _trace = trace;
    }

    public void Stop() => _trace.Stop();

    public void LogMetric(string metricName, long value) => _trace.SetIntValue(value, metricName);

    public void SetAttribute(string attributeName, string value) => _trace.SetValue(value, attributeName);
}