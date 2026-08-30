# Soenneker.Maui.Firebase.Performance
[![](https://img.shields.io/nuget/v/soenneker.maui.firebase.performance.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.firebase.performance/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.firebase.performance/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.maui.firebase.performance/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.maui.firebase.performance.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.maui.firebase.performance/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.maui.firebase.performance/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.maui.firebase.performance/actions/workflows/codeql.yml)

Provides Android and iOS services for custom Firebase Performance Monitoring traces in a .NET MAUI app.

## Installation

```bash
dotnet add package Soenneker.Maui.Firebase.Performance
```

Configure the native Firebase app first, including the platform's `google-services.json` or `GoogleService-Info.plist`.

## Registration

Enable collection through the Firebase builder and register the trace service:

```csharp
using Soenneker.Maui.Firebase.Dtos;
using Soenneker.Maui.Firebase.Performance.Registrars;
using Soenneker.Maui.Firebase.Registrars;

builder.UseFirebase(new FirebaseConfig())
       .EnableFirebasePerformance(collectionEnabled: userAllowsPerformanceMonitoring)
       .Build();

builder.Services.AddFirebasePerformanceServiceAsSingleton();
```

The service is registered only for Android and iOS targets. Collection can also be changed later with `SetPerformanceMonitoringEnabled`.

## Measure an operation

`Measure` always stops and disposes its trace, including when the operation fails:

```csharp
await performance.Measure("load_catalog", async () =>
{
    await catalog.Load();
});
```

## Manage a trace

Dispose manually-created traces. Disposal stops the trace and releases its native handle:

```csharp
using IFirebasePerformanceTrace trace = performance.StartTrace("sync_orders");

trace.SetAttribute("source", "background");
trace.LogMetric("orders", orderCount);

await orders.Sync();
```

Import `Soenneker.Maui.Firebase.Performance.Abstract` for the service and trace interfaces.

`LogMetric(name, value)` on the service creates a short-lived trace with the same trace and metric name. Use `StartTrace` when multiple metrics or attributes belong to one operation. Trace names, metric names, values, and attributes remain subject to Firebase's naming and quota limits.
