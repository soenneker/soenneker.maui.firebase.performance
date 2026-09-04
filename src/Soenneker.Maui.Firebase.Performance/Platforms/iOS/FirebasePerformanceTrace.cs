using Firebase.PerformanceMonitoring;
using Soenneker.Maui.Firebase.Performance.Abstract;
using System;
using System.Threading;

namespace Soenneker.Maui.Firebase.Performance.Platforms.iOS;

/// <inheritdoc cref="IFirebasePerformanceTrace" />
public sealed class FirebasePerformanceTrace : IFirebasePerformanceTrace
{
    private readonly Trace _trace;
    private int _stopped;
    private int _disposed;

    public FirebasePerformanceTrace(Trace trace)
    {
        _trace = trace;
    }

    public void Stop()
    {
        if (Interlocked.Exchange(ref _stopped, 1) == 0)
            _trace.Stop();
    }

    public void LogMetric(string metricName, long value)
    {
        EnsureActive();
        _trace.SetIntValue(value, metricName);
    }

    public void SetAttribute(string attributeName, string value)
    {
        EnsureActive();
        _trace.SetValue(value, attributeName);
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        try
        {
            Stop();
        }
        finally
        {
            _trace.Dispose();
        }
    }

    private void EnsureActive()
    {
        if (Volatile.Read(ref _stopped) != 0 || Volatile.Read(ref _disposed) != 0)
            throw new InvalidOperationException("The Firebase performance trace has already stopped.");
    }
}
