[![](https://img.shields.io/nuget/v/soenneker.maui.firebase.performance.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.firebase.performance/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.firebase.performance/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.maui.firebase.performance/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.maui.firebase.performance.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.firebase.performance/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.firebase.performance/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.maui.firebase.performance/actions/workflows/codeql.yml)

# Soenneker.Maui.Firebase.Performance

Provides an interface for Firebase Performance Monitoring.

## Install

```bash
dotnet add package Soenneker.Maui.Firebase.Performance
```

## Quick start

```csharp
using Soenneker.Maui.Firebase.Performance.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddFirebasePerformanceServiceAsSingleton();
```

Registers Firebase Performance Service with a singleton lifetime.

## What you get

- `IFirebasePerformanceService` — Provides an interface for Firebase Performance Monitoring.
- `IFirebasePerformanceTrace` — Represents a performance trace that can be used to measure app performance.
- `FirebasePerformanceServiceRegistrar` — Represents the firebase performance service registrar.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IFirebasePerformanceService.StartTrace(traceName)` | Starts a performance trace. | An abstracted trace object. |
| `IFirebasePerformanceService.LogMetric(metricName, value)` | Logs a custom metric globally without an explicit trace. | Returns no value; the requested change is complete when the method returns. |
| `IFirebasePerformanceService.SetAttribute(trace, attributeName, value)` | Sets a custom attribute on an existing trace. | Returns no value; the requested change is complete when the method returns. |
| `IFirebasePerformanceService.Measure(traceName, operation)` | Measures execution time of an async operation. | A task that completes when the measure operation is complete. |
| `IFirebasePerformanceService.IsPerformanceMonitoringEnabled()` | Checks whether Firebase Performance Monitoring is enabled. | True if performance monitoring is enabled, otherwise false. |
| `IFirebasePerformanceService.SetPerformanceMonitoringEnabled(enabled)` | Enables or disables Firebase Performance Monitoring at runtime. | Returns no value; the requested change is complete when the method returns. |
| `IFirebasePerformanceTrace.LogMetric(metricName, value)` | Logs a custom metric to an existing trace. | Returns no value; the requested change is complete when the method returns. |
| `FirebasePerformanceServiceRegistrar.AddFirebasePerformanceServiceAsSingleton(services)` | Registers Firebase Performance Service with a singleton lifetime. | The same service collection, so additional registrations can be chained. |
| `FirebasePerformanceServiceRegistrar.EnableFirebasePerformance(builder)` | Adds the enable firebase performance firebase performance service utility to the class list. | The same builder instance, so additional classes or variants can be chained. |
