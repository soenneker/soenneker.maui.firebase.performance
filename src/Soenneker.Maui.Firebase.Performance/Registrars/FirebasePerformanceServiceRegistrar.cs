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
    /// Adds firebase performance service as singleton.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
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
    /// Executes the enable firebase performance operation.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The result of the operation.</returns>
    public static FirebaseMauiBuilder EnableFirebasePerformance(this FirebaseMauiBuilder builder)
    {
        return builder.AddService((firebaseInstance, config) =>
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