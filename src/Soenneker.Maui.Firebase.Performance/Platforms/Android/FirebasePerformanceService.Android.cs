using Firebase.Perf;
using Firebase.Perf.Metrics;
using Soenneker.Maui.Firebase.Performance.Abstract;
using System;
using System.Threading.Tasks;

namespace Soenneker.Maui.Firebase.Performance.Platforms.Android;

/// <inheritdoc cref="IFirebasePerformanceService" />
public sealed class FirebasePerformanceService : IFirebasePerformanceService
{
    public IFirebasePerformanceTrace StartTrace(string traceName)
    {
        Trace trace = FirebasePerformance.Instance.NewTrace(traceName);
        trace.Start();
        return new FirebasePerformanceTrace(trace);
    }

    public void StopTrace(IFirebasePerformanceTrace trace)
    {
        trace.Dispose();
    }

    public void LogMetric(string metricName, long value)
    {
        using Trace trace = FirebasePerformance.Instance.NewTrace(metricName);
        trace.Start();

        try
        {
            trace.PutMetric(metricName, value);
        }
        finally
        {
            trace.Stop();
        }
    }

    public void SetAttribute(IFirebasePerformanceTrace trace, string attributeName, string value)
    {
        trace.SetAttribute(attributeName, value);
    }

    public async Task Measure(string traceName, Func<Task> operation)
    {
        IFirebasePerformanceTrace trace = StartTrace(traceName);

        try
        {
            await operation();
        }
        finally
        {
            trace.Dispose();
        }
    }

    public bool IsPerformanceMonitoringEnabled()
    {
        return FirebasePerformance.Instance.PerformanceCollectionEnabled;
    }

    public void SetPerformanceMonitoringEnabled(bool enabled)
    {
        FirebasePerformance.Instance.PerformanceCollectionEnabled = enabled;
    }
}
