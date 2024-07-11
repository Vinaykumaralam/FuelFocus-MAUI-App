using FuelFocus.View;

namespace FuelFocus
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(ForgotPassPage), typeof(ForgotPassPage));
            Routing.RegisterRoute(nameof(OTPPage), typeof(OTPPage));
            Routing.RegisterRoute(nameof(NewPasswordPage), typeof(NewPasswordPage));
            Routing.RegisterRoute(nameof(RoutePage), typeof(RoutePage));
            Routing.RegisterRoute(nameof(TripDetailPage), typeof(TripDetailPage));
            Routing.RegisterRoute(nameof(RecentTripsPage), typeof(RecentTripsPage));

        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            Preferences.Remove("IsLoggedIn");
            Preferences.Remove("UserMobileNo");
            Preferences.Remove("UserName");
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
