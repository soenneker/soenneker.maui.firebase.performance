using Microsoft.Extensions.DependencyInjection;
using Soenneker.Maui.Firebase.Performance.Abstract;

#if ANDROID
using Soenneker.Maui.Firebase.Performance.Platforms.Android;
using Firebase.Perf;
#endif

#if IOS
using Soenneker.Maui.Firebase.Performance.Platforms.iOS;
#endif

namespace Soenneker.Maui.Firebase.Performance.Registrars;

/// <summary>
/// Represents the firebase performance service registrar.
/// </summary>
public static class FirebasePerformanceServiceRegistrar
{
    /// <summary>
    /// Registers Firebase Performance Service with a singleton lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddFirebasePerformanceServiceAsSingleton(this IServiceCollection services)
    {
#if ANDROID
        services.AddSingleton<IFirebasePerformanceService, FirebasePerformanceService>();
#endif
#if IOS
        services.AddSingleton<IFirebasePerformanceService, FirebasePerformanceService>();
#endif
        return services;
    }

    /// <summary>
    /// Adds the enable firebase performance firebase performance service utility to the class list.
    /// </summary>
    /// <param name="builder">Builder to configure.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static FirebaseMauiBuilder EnableFirebasePerformance(this FirebaseMauiBuilder builder)
    {
#pragma warning disable CA1416 // The referenced Firebase package has malformed platform metadata; this project is platform-targeted.
        return builder.AddService((firebaseInstance, config) =>
#pragma warning restore CA1416
        {
#if ANDROID
            FirebasePerformance.Instance.PerformanceCollectionEnabled = true;
#endif

#if IOS
            global::Firebase.PerformanceMonitoring.Performance.SharedInstance.DataCollectionEnabled = true;
#endif
        });
    }
}
