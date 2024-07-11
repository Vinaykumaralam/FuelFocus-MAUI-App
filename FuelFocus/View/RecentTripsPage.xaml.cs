namespace FuelFocus.View;

using FuelFocus.ViewModel;
using FuelFocus.Model;
using Newtonsoft.Json;

public partial class RecentTripsPage : ContentPage
{
    //private readonly TripsViewModel _tripsViewModel;
    private string BaseUrl = "http://10.0.2.2:5183";
    
    public RecentTripsPage()
    {
        InitializeComponent();
        string UserName = Preferences.Get("UserName", "User");
        DisplayAlert("Welcome " + UserName, "\nYour Trip history..", "Ok");
        var viewModel = new TripsViewModel();
    }

    public async void LoadData()
    {
        try
        {
            var req = new HttpClient();
            var tripDetails = await req.GetAsync(BaseUrl + "/Trip/GetRecords");
            var vehicleID = Preferences.Get("VehicleId", 1);
            if (tripDetails.IsSuccessStatusCode)
            {
                var content = await tripDetails.Content.ReadAsStringAsync();
                List<TripDetail> records = JsonConvert.DeserializeObject<List<TripDetail>>(content);
                List<TripDetail> tripList = records.Where(X => X.VehicleID == vehicleID).ToList();
                TripListView.ItemsSource = tripList;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}