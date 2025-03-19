using Microsoft.Extensions.DependencyInjection;
using Soenneker.Maui.Firebase.Performance.Abstract;

#if ANDROID
using Soenneker.Maui.Firebase.Performance.Platforms.Android;
#endif

#if IOS
using Soenneker.Maui.Firebase.Performance.Platforms.iOS;
#endif

namespace Soenneker.Maui.Firebase.Performance.Registrars;

public static class FirebasePerformanceServiceRegistrar
{
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