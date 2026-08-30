using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Maui.Firebase.Performance.Abstract;

#if ANDROID
using Firebase.Perf;
using Soenneker.Maui.Firebase.Performance.Platforms.Android;
#endif

#if IOS
using Soenneker.Maui.Firebase.Performance.Platforms.iOS;
#endif

namespace Soenneker.Maui.Firebase.Performance.Registrars;

/// <summary>
/// Registers and configures Firebase Performance Monitoring.
/// </summary>
public static class FirebasePerformanceServiceRegistrar
{
    /// <summary>
    /// Registers the Firebase Performance service with a singleton lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddFirebasePerformanceServiceAsSingleton(this IServiceCollection services)
    {
#if ANDROID
        services.TryAddSingleton<IFirebasePerformanceService, FirebasePerformanceService>();
#endif
#if IOS
        services.TryAddSingleton<IFirebasePerformanceService, FirebasePerformanceService>();
#endif
        return services;
    }

    /// <summary>
    /// Configures Firebase Performance collection during native Firebase initialization.
    /// </summary>
    /// <param name="builder">Firebase builder to configure.</param>
    /// <param name="collectionEnabled">Whether the native SDK may collect performance data.</param>
    /// <returns>The same builder instance, so additional integrations can be chained.</returns>
    public static FirebaseMauiBuilder EnableFirebasePerformance(this FirebaseMauiBuilder builder, bool collectionEnabled = true)
    {
#pragma warning disable CA1416 // The referenced Firebase package has malformed platform metadata; this project is platform-targeted.
        return builder.AddService((firebaseInstance, config) =>
#pragma warning restore CA1416
        {
#if ANDROID
            FirebasePerformance.Instance.PerformanceCollectionEnabled = collectionEnabled;
#endif

#if IOS
            global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled = collectionEnabled;
#endif
        });
    }
}
