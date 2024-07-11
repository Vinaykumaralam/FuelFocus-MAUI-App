using DocumentFormat.OpenXml.Bibliography;
using FuelFocus.Services;
using FuelFocus.ViewModel;
using Microsoft.Maui.Devices.Sensors;
using Syncfusion.Maui.Gauges;

namespace FuelFocus.View;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
       
        BindingContext = new HomeViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        string name = Preferences.Get("UserName", "User");
        Username.Text = GetDisplayUsername(name);
        string mobileNo = Preferences.Get("UserMobileNo", "9392658469");
  
        if (BindingContext is HomeViewModel viewModel)
        {
            await viewModel.LoadAsyncData();
        }

    }
    private async void ProfileImage_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
    }

    private async void Reports_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(RecentTripsPage)}");

        }
        catch (System.ObjectDisposedException ex)
        {
            throw;
        }

    }

    private async void StartTrip_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(RoutePage)}");
    }
    string GetDisplayUsername(string name)
    {
        var words = name.Split(' ');
        if (words.Length == 1)
        {
            return name.Length <= 8 ? name : name.Substring(0, 8);
        }
        else
        {
            string firstWord = words[0];
            return firstWord.Length <= 8 ? firstWord : firstWord.Substring(0, 8);
        }
    }
}