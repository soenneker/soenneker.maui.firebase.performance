using System;
using System.Threading.Tasks;
using Firebase.Perf;
using Firebase.Perf.Metrics;
using Soenneker.Maui.Firebase.Performance.Abstract;

namespace Soenneker.Maui.Firebase.Performance.Platforms.Android;

public class FirebasePerformanceService : IFirebasePerformanceService
{
    public IFirebasePerformanceTrace StartTrace(string traceName)
    {
        Trace trace = FirebasePerformance.Instance.NewTrace(traceName);
        trace.Start();
        return new FirebasePerformanceTrace(trace);
    }

    public void StopTrace(IFirebasePerformanceTrace trace)
    {
        trace.Stop();
    }

    public void LogMetric(string metricName, long value)
    {
        Trace trace = FirebasePerformance.Instance.NewTrace(metricName);
        trace.Start();
        trace.PutMetric(metricName, value);
        trace.Stop();
    }

    public void SetAttribute(IFirebasePerformanceTrace trace, string attributeName, string value)
    {
        if (trace is FirebasePerformanceTrace firebaseTrace)
        {
            firebaseTrace.SetAttribute(attributeName, value);
        }
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
            trace.Stop();
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
