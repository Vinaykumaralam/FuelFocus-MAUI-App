using CommunityToolkit.Mvvm.Input;
using FuelFocus.Services;
using Microsoft.Maui.Devices.Sensors;

namespace FuelFocus.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    
    private readonly ILoginService _loginService;

    private double _fuelTankVolume;
    public double FuelTankVolume
    {
        get => _fuelTankVolume;
        set => SetProperty(ref _fuelTankVolume, value);
    }

    private double _vehicleMileage;
    public double VehicleMileage
    {
        get => _vehicleMileage;
        set => SetProperty(ref _vehicleMileage, value);
    }

    private double _currentFuelLevel;
    public double CurrentFuelLevel
    {
        get => _currentFuelLevel;
        set => SetProperty(ref _currentFuelLevel, value);
    }

    private double _radialMax;
    public double RadialMax
    {
        get => _radialMax;
        set => SetProperty(ref _radialMax, value);
    }

    private string _username;
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }
    private double _estimated;
    public double Estimated
    {
        get => _estimated;
        set => SetProperty(ref _estimated, value);
    }

    public HomeViewModel()
    {
        _loginService = new LoginService();
    }
    public async Task LoadAsyncData()
    {
        try
        {
            Username = Preferences.Get("UserName", "User");
            string mobileNo = Preferences.Get("UserMobileNo", "9392658469");
            if (_loginService != null)
            {
                var vehicle = await _loginService.GetVehicle(long.Parse(mobileNo));
                var vehicleID = vehicle.VehicleID;
                Preferences.Set("VehicleId", vehicleID);
                if (vehicle != null)
                {
                    CurrentFuelLevel = vehicle.CurrentOdoMeter != 0 ? vehicle.CurrentOdoMeter : 0;
                    VehicleMileage = vehicle.VehicleMilage != 0 ? vehicle.VehicleMilage : 0;
                    FuelTankVolume = vehicle.FuelTankVolume != 0 ? vehicle.FuelTankVolume : 30;

                    if (vehicle.CurrentOdoMeter != 0 && vehicle.VehicleMilage != 0)
                    {
                        Estimated = vehicle.CurrentOdoMeter * vehicle.VehicleMilage;
                    }
                    else
                    {
                        Estimated = 0;
                    }
                    RadialMax = FuelTankVolume;
                }
                else
                {
                    // Handle case where vehicle data couldn't be fetched
                }
            }
            else
            {
                // Handle case where _loginService is not initialized
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            throw;
        }
    }

    //public async Task LoadAsyncData()
    //{
    //    try
    //    {
    //        Username = Preferences.Get("UserName", "User");
    //        string mobileNo = Preferences.Get("UserMobileNo", "9392658469");
    //        if (_loginService != null)
    //        {
    //            var vehicle = await _loginService.GetVehicle(long.Parse(mobileNo));

    //            if (vehicle != null)
    //            {
    //                CurrentFuelLevel = vehicle.CurrentOdoMeter;
    //                VehicleMileage = vehicle.VehicleMilage;
    //                FuelTankVolume = vehicle.FuelTankVolume;
    //                if (vehicle.CurrentOdoMeter != 0 && vehicle.VehicleMilage != 0)
    //                {
    //                    Estimated = vehicle.CurrentOdoMeter * vehicle.VehicleMilage;
    //                }
    //                RadialMax = 30;
    //            }
    //            else
    //            {
    //                // Handle case where vehicle data couldn't be fetched
    //            }
    //        }
    //        else
    //        {
    //            // Handle case where _loginService is not initialized
    //        }
    //    }catch(Exception ex)
    //    {
    //        throw;
    //    }
    //}

    [RelayCommand]
    public async Task Location_Tapped()
    {
        Location location = await Geolocation.GetLastKnownLocationAsync();
        if (location == null)
        {
            location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30),
            });

            if (location == null)
            {
                await Shell.Current.DisplayAlert("Error", "Unable to determine location", "Ok");
                return;
            }
        }
        await Map.OpenAsync(location, new MapLaunchOptions
        {
            Name = "Current Location",
            NavigationMode = NavigationMode.None
        });
    }
}