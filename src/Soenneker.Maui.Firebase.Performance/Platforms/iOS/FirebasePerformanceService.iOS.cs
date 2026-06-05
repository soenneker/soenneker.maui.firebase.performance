using Soenneker.Maui.Firebase.Performance.Abstract;
using System;
using Firebase.PerformanceMonitoring;
using System.Threading.Tasks;

namespace Soenneker.Maui.Firebase.Performance.Platforms.iOS;

/// <inheritdoc cref="IFirebasePerformanceService"/>
public class FirebasePerformanceService : IFirebasePerformanceService
{
    public IFirebasePerformanceTrace StartTrace(string traceName)
    {
        Trace? trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(traceName);
        
        return new FirebasePerformanceTrace(trace!);
    }

    public void StopTrace(IFirebasePerformanceTrace trace)
    {
        trace.Stop();
    }

    public void LogMetric(string metricName, long value)
    {
        Trace? trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(metricName);
        trace!.SetIntValue(value, metricName);
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
        return global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled;
    }

    public void SetPerformanceMonitoringEnabled(bool enabled)
    {
        global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled = enabled;
    }

}