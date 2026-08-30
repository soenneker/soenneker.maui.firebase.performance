using Firebase.PerformanceMonitoring;
using Soenneker.Maui.Firebase.Performance.Abstract;
using System;
using System.Threading.Tasks;

namespace Soenneker.Maui.Firebase.Performance.Platforms.iOS;

public sealed class FirebasePerformanceService : IFirebasePerformanceService
{
    public IFirebasePerformanceTrace StartTrace(string traceName)
    {
        Trace trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(traceName)
                      ?? throw new InvalidOperationException($"Firebase could not start the performance trace '{traceName}'.");

        return new FirebasePerformanceTrace(trace);
    }

    public void StopTrace(IFirebasePerformanceTrace trace)
    {
        trace.Dispose();
    }

    public void LogMetric(string metricName, long value)
    {
        using Trace trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(metricName)
                            ?? throw new InvalidOperationException($"Firebase could not start the performance trace '{metricName}'.");

        try
        {
            trace.SetIntValue(value, metricName);
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
        return global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled;
    }

    public void SetPerformanceMonitoringEnabled(bool enabled)
    {
        global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled = enabled;
    }
}
