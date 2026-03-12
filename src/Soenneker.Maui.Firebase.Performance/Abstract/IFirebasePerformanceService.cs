using System;
using System.Threading.Tasks;

namespace Soenneker.Maui.Firebase.Performance.Abstract;

/// <summary>
/// A cross-platform library for adding Firebase Performance to MAUI applications
/// </summary>
/// <summary>
/// Provides an interface for Firebase Performance Monitoring.
/// </summary>
/// <summary>
/// Provides an interface for Firebase Performance Monitoring.
/// </summary>
public interface IFirebasePerformanceService
{
    /// <summary>
    /// Starts a performance trace.
    /// </summary>
    /// <param name="traceName">Name of the trace</param>
    /// <returns>An abstracted trace object</returns>
    IFirebasePerformanceTrace StartTrace(string traceName);

    /// <summary>
    /// Stops a running performance trace.
    /// </summary>
    /// <param name="trace">The trace object to stop.</param>
    void StopTrace(IFirebasePerformanceTrace trace);

    /// <summary>
    /// Logs a custom metric globally without an explicit trace.
    /// </summary>
    /// <param name="metricName">The name of the metric.</param>
    /// <param name="value">The value of the metric.</param>
    void LogMetric(string metricName, long value);

    /// <summary>
    /// Sets a custom attribute on an existing trace.
    /// </summary>
    /// <param name="trace">The trace object.</param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    void SetAttribute(IFirebasePerformanceTrace trace, string attributeName, string value);

    /// <summary>
    /// Measures execution time of an async operation.
    /// </summary>
    /// <param name="traceName">Name of the trace</param>
    /// <param name="operation">Async operation</param>
    Task Measure(string traceName, Func<Task> operation);

    /// <summary>
    /// Checks whether Firebase Performance Monitoring is enabled.
    /// </summary>
    /// <returns>True if performance monitoring is enabled, otherwise false.</returns>
    bool IsPerformanceMonitoringEnabled();

    /// <summary>
    /// Enables or disables Firebase Performance Monitoring at runtime.
    /// </summary>
    /// <param name="enabled">True to enable, false to disable.</param>
    void SetPerformanceMonitoringEnabled(bool enabled);
}