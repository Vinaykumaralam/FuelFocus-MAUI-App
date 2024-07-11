using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using FuelFocus.Services;
using FuelFocus.View;
using FuelFocus.ViewModel;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using The49.Maui.ContextMenu;

namespace FuelFocus;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .UseContextMenu()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<SignupPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<RoutePage>();
        builder.Services.AddSingleton<ForgotPassPage>();
        builder.Services.AddSingleton<NewPasswordPage>();
        builder.Services.AddSingleton<OTPPage>();
        builder.Services.AddSingleton<TripDetailPage>();
        builder.Services.AddSingleton<RecentTripsPage>();

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<SignUpViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<RouteViewModel>();
        builder.Services.AddSingleton<VehicleReportViewModel>();
        builder.Services.AddSingleton<RouteViewModel>();
        builder.Services.AddSingleton<TripsViewModel>();

        builder.Services.AddHttpClient<ILoginService, LoginService>();
        builder.Services.AddHttpClient<ISignUpService, SignUpService>();
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IFileSaver>( FileSaver.Default);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
