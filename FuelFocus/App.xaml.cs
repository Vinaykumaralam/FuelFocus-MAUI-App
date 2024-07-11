using FuelFocus.Services;
using FuelFocus.View;

namespace FuelFocus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            CheckLoginState();
        }
        private async void CheckLoginState()
        {
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            //bool isLoggedIn = false;

            if (isLoggedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }
    }
}
