using FuelFocus.ViewModel;
using static System.Net.WebRequestMethods;

namespace FuelFocus.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is VehicleViewModel viewModel)
        {
            await viewModel.FetchVehiclesFromApi();
        }

    }
}