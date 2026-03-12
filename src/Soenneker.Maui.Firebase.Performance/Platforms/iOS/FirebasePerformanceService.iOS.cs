using Soenneker.Maui.Firebase.Performance.Abstract;
using System;
using Firebase.PerformanceMonitoring;
using System.Threading.Tasks;

namespace Soenneker.Maui.Firebase.Performance.Platforms.iOS;

public class FirebasePerformanceService : IFirebasePerformanceService
{
    /// <summary>
    /// Starts a performance trace using Firebase's shared instance.
    /// </summary>
    public IFirebasePerformanceTrace StartTrace(string traceName)
    {
        Trace? trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(traceName);
        
        return new FirebasePerformanceTrace(trace!);
    }

    /// <summary>
    /// Stops a running trace.
    /// </summary>
    public void StopTrace(IFirebasePerformanceTrace trace)
    {
        trace.Stop();
    }

    /// <summary>
    /// Logs a standalone metric by creating and stopping a trace immediately.
    /// </summary>
    public void LogMetric(string metricName, long value)
    {
        Trace? trace = global::Firebase.PerformanceMonitoring.Performance.StartTrace(metricName);
        trace!.SetIntValue(value, metricName);
        trace.Stop();
    }

    /// <summary>
    /// Adds a custom attribute to an existing trace.
    /// </summary>
    public void SetAttribute(IFirebasePerformanceTrace trace, string attributeName, string value)
    {
        if (trace is FirebasePerformanceTrace firebaseTrace)
        {
            firebaseTrace.SetAttribute(attributeName, value);
        }
    }

    /// <summary>
    /// Measures execution time of an async operation and logs it to Firebase Performance.
    /// </summary>
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