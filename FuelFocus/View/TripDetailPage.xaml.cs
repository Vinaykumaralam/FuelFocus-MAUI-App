using FuelFocus.Model;
using FuelFocus.ViewModel;
using Newtonsoft.Json.Linq;
using System.Globalization;
namespace FuelFocus.View;

public partial class TripDetailPage : ContentPage
{
    //private readonly RouteViewModel _routeViewModel;
    private static readonly HttpClient client = new HttpClient();
    public TripDetailPage(TripDetail item)
    {
        InitializeComponent();
        StatDdl.SelectedIndex = 0;
        SourceLoc.Text = item.Source;
        DestinationLoc.Text = item.Destination;
        MapWebView.Navigated += MapWebView_Loaded;
        BindingContext = new VehicleReportViewModel();
        //_routeViewModel = new RouteViewModel();
        LoadData(item);


    }
    public void LoadData(TripDetail item)
    {
        var items = new List<CarouselContent>
     {
         new CarouselContent{Title="CONSUMPTION",Value=(int)item.FuelConsumed,Kilometer=(int)item.Distance,Refilling=50},
         new CarouselContent{Title="EXISTING",Value=(int)item.FuelConsumed,Kilometer=(int)item.Distance,Refilling=50},
         new CarouselContent{Title="BALANCE",Value=(int)item.FuelConsumed,Kilometer=(int)item.Distance,Refilling = 50},
         new CarouselContent{Title="REFILL",Value=(int)item.FuelConsumed,Kilometer=(int)item.Distance,Refilling=50}
     };
        ReportCV.ItemsSource = items;
        if (Application.Current.Resources.TryGetValue("TripId", out var TripId))
        {
            Application.Current.Resources.Remove("TripId");
            Application.Current.Resources.Add("TripId", item.TripId);
        }
        else
        {
            Application.Current.Resources.Add("TripId", item.TripId);
        }
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string? selectedValue = StatDdl.SelectedItem.ToString();

            if (selectedValue == "Statistics")
            {
                //StatisticsGraph.IsVisible = true;
                //StatisticsPieGraph.IsVisible = false;
            }
            else if (selectedValue == "Consumption")
            {
                //StatisticsPieGraph.IsVisible = true;
                //StatisticsGraph.IsVisible = false;
            }

        }
        catch (Exception ex) { throw; }
    }
    private async void MapWebView_Loaded(object sender, WebNavigatedEventArgs e)
    {
        try
        {

            string Source = SourceLoc.Text;
            string Destination = DestinationLoc.Text;

            var SourceCoordinates = await GetCoordinates(Source);
            double S_latitude = SourceCoordinates.Latitude;
            double S_longitude = SourceCoordinates.Longitude;

            var DestCoordinates = await GetCoordinates(Destination);
            double D_latitude = DestCoordinates.Latitude;
            double D_longitude = DestCoordinates.Longitude;

            string distance = await CalculateDist(S_latitude, S_longitude, D_latitude, D_longitude);
            double dist = double.Parse(distance) / 1000;
            string script = $"updateMap([{S_latitude}, {S_longitude}], [{D_latitude},{D_longitude}]);";
            await MapWebView.EvaluateJavaScriptAsync(script);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public async Task<(double Latitude, double Longitude)> GetCoordinates(string address)
    {
        try
        {
            //var requestUri = $"https://api.opencagedata.com/geocode/v1/json?q={Uri.EscapeDataString(address)}&key={OpenCageApiKey}";
            var requestUri = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";
            HttpResponseMessage response = await client.GetAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JArray.Parse(responseBody);

            if (json.Count > 0)
            {
                var location = json[0];
                double latitude = (double)location["lat"];
                double longitude = (double)location["lon"];

                return (latitude, longitude);
            }
            else
            {
                throw new Exception("Location not found");
            }
        }
        catch (HttpRequestException hex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<string> CalculateDist(double S_latitude, double S_longitude, double D_latitude, double D_longitude)
    {
        try
        {
            var distance = "";
            var request = $"http://router.project-osrm.org/route/v1/driving/{S_longitude},{S_latitude};{D_longitude},{D_latitude}?overview=false";
            HttpResponseMessage response = await client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseData);
                distance = jsonResponse["routes"][0]["distance"].ToString();
                var duration = jsonResponse["routes"][0]["duration"];
                return distance;
            }
            else
            {
                return distance;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async void OpenMaps_Event(object sender, TappedEventArgs e)
    {
        string Source = SourceLoc.Text;
        string Destination = DestinationLoc.Text;
        var SourceCoordinates = await GetCoordinates(Source);
        double S_latitude = SourceCoordinates.Latitude;
        double S_longitude = SourceCoordinates.Longitude;

        var DestCoordinates = await GetCoordinates(Destination);
        double D_latitude = DestCoordinates.Latitude;
        double D_longitude = DestCoordinates.Longitude;
        var fromLocation = new Location(S_latitude, S_longitude);
        var toLocation = new Location(D_latitude, D_longitude);

        try
        {
            var fromLatitude = fromLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var fromLongitude = fromLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            var toLatitude = toLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var toLongitude = toLocation.Longitude.ToString(CultureInfo.InvariantCulture);

            var uri = $"https://www.google.com/maps/dir/?api=1&origin={fromLatitude},{fromLongitude}&destination={toLatitude},{toLongitude}&travelmode=driving";
            await Launcher.OpenAsync(new Uri(uri));
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string Source = SourceLoc.Text;
        string Destination = DestinationLoc.Text;
        var SourceCoordinates = await GetCoordinates(Source);
        double S_latitude = SourceCoordinates.Latitude;
        double S_longitude = SourceCoordinates.Longitude;

        var DestCoordinates = await GetCoordinates(Destination);
        double D_latitude = DestCoordinates.Latitude;
        double D_longitude = DestCoordinates.Longitude;
        var fromLocation = new Location(S_latitude, S_longitude);
        var toLocation = new Location(D_latitude, D_longitude);

        try
        {
            var fromLatitude = fromLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var fromLongitude = fromLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            var toLatitude = toLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var toLongitude = toLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            var uri = $"https://www.google.com/maps/dir/?api=1&origin={fromLatitude},{fromLongitude}&destination={toLatitude},{toLongitude}&travelmode=driving";
            await Launcher.OpenAsync(new Uri(uri));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}