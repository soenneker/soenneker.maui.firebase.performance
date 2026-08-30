using System;

namespace Soenneker.Maui.Firebase.Performance.Abstract;

/// <summary>
/// Represents a running Firebase Performance Monitoring trace.
/// </summary>
public interface IFirebasePerformanceTrace : IDisposable
{
    /// <summary>
    /// Stops the trace. Calling this more than once has no effect.
    /// </summary>
    void Stop();

    /// <summary>
    /// Records a custom metric on the trace.
    /// </summary>
    /// <param name="metricName">Name of the metric.</param>
    /// <param name="value">Metric value.</param>
    void LogMetric(string metricName, long value);

    /// <summary>
    /// Sets an attribute on the trace.
    /// </summary>
    /// <param name="attributeName">Name of the attribute.</param>
    /// <param name="value">Attribute value.</param>
    void SetAttribute(string attributeName, string value);
}
