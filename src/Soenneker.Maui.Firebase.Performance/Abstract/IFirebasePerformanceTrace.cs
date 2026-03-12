namespace Soenneker.Maui.Firebase.Performance.Abstract;

/// <summary>
/// Represents a performance trace that can be used to measure app performance.
/// </summary>
public interface IFirebasePerformanceTrace
{
    /// <summary>
    /// Stops the trace.
    /// </summary>
    void Stop();

    /// <summary>
    /// Logs a custom metric to an existing trace.
    /// </summary>
    /// <param name="metricName">Name of the metric</param>
    /// <param name="value">Metric value</param>
    void LogMetric(string metricName, long value);
}