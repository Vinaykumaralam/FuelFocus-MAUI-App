using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FuelFocus.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using FuelFocus.Model;

namespace FuelFocus.ViewModel
{
    public class RouteViewModel : INotifyPropertyChanged
    {
        private WebView _mapWebView;
        public Command BackCommand { get; set; }

        private readonly HttpClient _httpClient;
        private readonly Uri BaseUrl = new Uri("http://10.0.2.2:5183/Trip");
        public RouteViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = BaseUrl };
            FuelStationsCommand = new Command(async () => await GetFuelStations());
            SwapLocationsCommand = new Command(SwapLocations);
            GetDirectionsCommand = new Command(async () => await GetDirections());
            StartTripCommand = new Command(async () => await StartTrip());
            BackCommand = new Command(async () => await Back());
            SetSourceToCurrentLocationCommand = new Command(async () => await SetSourceToCurrentLocation());
        }

        public void SetWebView(WebView mapWebView)
        {
            _mapWebView = mapWebView;
        }

        private string _sourceLocation;
        public string SourceLocation
        {
            get => _sourceLocation;
            set
            {
                _sourceLocation = value;
                OnPropertyChanged();
            }
        }

        private string _destinationLocation;
        public string DestinationLocation
        {
            get => _destinationLocation;
            set
            {
                _destinationLocation = value;
                OnPropertyChanged();
            }
        }

        /* private decimal _fuelLeft;
         public decimal FuelLeft
         {
             get => _fuelLeft;
             set
             {
                 _fuelLeft = value;
                 OnPropertyChanged();
             }
         }*/

        public ICommand FuelStationsCommand { get; }
        public ICommand SwapLocationsCommand { get; }
        public ICommand GetDirectionsCommand { get; }
        public ICommand StartTripCommand { get; }
        public ICommand SetSourceToCurrentLocationCommand { get; }

        private bool IsNetworkAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        private void SwapLocations()
        {
            var temp = SourceLocation;
            SourceLocation = DestinationLocation;
            DestinationLocation = temp;
        }

        private async Task GetFuelStations()
        {
            try
            {
                Location location = null;

                if (!string.IsNullOrEmpty(SourceLocation) && !string.IsNullOrEmpty(DestinationLocation))
                {
                    var sourceCoordinates = await GetCoordinates(SourceLocation);
                    var destinationCoordinates = await GetCoordinates(DestinationLocation);

                    double avgLatitude = (sourceCoordinates.Latitude + destinationCoordinates.Latitude) / 2;
                    double avgLongitude = (sourceCoordinates.Longitude + destinationCoordinates.Longitude) / 2;

                    location = new Location(avgLatitude, avgLongitude);
                }
                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    location = await Geolocation.GetLocationAsync(request);
                }

                if (location != null)
                {
                    var latitude = location.Latitude.ToString(CultureInfo.InvariantCulture);
                    var longitude = location.Longitude.ToString(CultureInfo.InvariantCulture);

                    var uri = $"https://www.google.com/maps/search/?api=1&query=fuel+stations&query_place_id={latitude},{longitude}";
                    await Launcher.OpenAsync(new Uri(uri));
                }
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public async Task<(double Latitude, double Longitude)> GetCoordinates(string address)
        {
            try
            {
                var requestUri = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
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
                HttpResponseMessage response = await _httpClient.GetAsync(request);
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

        public async Task GetDirections()
        {
            await MapWebViewNavigated();
        }

        public async Task MapWebViewNavigated()
        {
            try
            {
                if (!string.IsNullOrEmpty(SourceLocation) && !string.IsNullOrEmpty(DestinationLocation))
                {
                    string Source = SourceLocation;
                    string Destination = DestinationLocation;

                    var SourceCoordinates = await GetCoordinates(Source);
                    double S_latitude = SourceCoordinates.Latitude;
                    double S_longitude = SourceCoordinates.Longitude;

                    var DestCoordinates = await GetCoordinates(Destination);
                    double D_latitude = DestCoordinates.Latitude;
                    double D_longitude = DestCoordinates.Longitude;

                    string distance = await CalculateDist(S_latitude, S_longitude, D_latitude, D_longitude);
                    string script = $"updateMap([{S_latitude}, {S_longitude}], [{D_latitude},{D_longitude}]);";
                    await _mapWebView.EvaluateJavaScriptAsync(script);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please provide both source and destination locations.", "OK");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task StartTrip()
        {
            try
            {
                if (!string.IsNullOrEmpty(SourceLocation) && !string.IsNullOrEmpty(DestinationLocation))
                {
                    var sourceCoordinates = await GetCoordinates(SourceLocation);
                    var destinationCoordinates = await GetCoordinates(DestinationLocation);

                    var sourceLat = sourceCoordinates.Latitude.ToString(CultureInfo.InvariantCulture);
                    var sourceLng = sourceCoordinates.Longitude.ToString(CultureInfo.InvariantCulture);
                    var destLat = destinationCoordinates.Latitude.ToString(CultureInfo.InvariantCulture);
                    var destLng = destinationCoordinates.Longitude.ToString(CultureInfo.InvariantCulture);

                    var uri = $"https://www.google.com/maps/dir/?api=1&origin={sourceLat},{sourceLng}&destination={destLat},{destLng}&travelmode=driving";
                    await Launcher.OpenAsync(new Uri(uri));

                    var distance = await CalculateDist(sourceCoordinates.Latitude, sourceCoordinates.Longitude, destinationCoordinates.Latitude, destinationCoordinates.Longitude);
                    var distanceKm = decimal.Parse(distance) / 1000;

                    var hours = Math.Floor((double)distanceKm / 60);
                    var minutes = Math.Floor(((double)distanceKm / 60 % 1) * 60);
                    var journeyTime = $"{hours}:{minutes}";

                    var endDateTime = DateTime.Now.AddHours(hours).AddMinutes(minutes);

                    var vehicleId = Preferences.Get("VehicleId", 1);
                    var vehicle = await GetVehicleById(vehicleId);
                    var fuelConsumed = distanceKm / (decimal)vehicle.VehicleMilage;
                    var fuelRemaining = (decimal)vehicle.CurrentOdoMeter - (decimal)fuelConsumed;

                    var tripDetail = new TripDetail
                    {
                        Source = SourceLocation,
                        Destination = DestinationLocation,
                        Distance = distanceKm,
                        FuelConsumed = fuelConsumed,
                        FuelRemaining = (decimal)fuelRemaining,
                        TotalFuel = (decimal)vehicle.CurrentOdoMeter,
                        StartDateTime = DateTime.Now,
                        VehicleID = vehicleId,
                        JourneyTime = journeyTime,
                        EndDateTime = endDateTime,
                        Created_On = DateTime.Now,
                        Updated_On = DateTime.Now,

                    };

                    await PostTripDetails(tripDetail);

                    await UpdateVehicleFuelLeft(vehicleId, (double)fuelRemaining);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please provide both source and destination locations.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task<Vehicle> GetVehicleById(int vehicleId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://10.0.2.2:5183/Vehicles/GetVehicles");
                Vehicle filterVehicle = new Vehicle();
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(data);
                    filterVehicle = vehicles.Where(x => x.VehicleID == vehicleId).FirstOrDefault();
                }
                return filterVehicle;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task PostTripDetails(TripDetail tripDetail)
        {
            try
            {

                var json = JsonConvert.SerializeObject(tripDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}/PostTrip", content);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Trip Started", "Your trip details have been saved.", "OK");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save trip details. Server responded with: {errorMessage}", "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task UpdateVehicleFuelLeft(int vehicleId, double fuelRemaining)
        {
            try
            {
                var vehicle = await GetVehicleById(vehicleId);
                vehicle.CurrentOdoMeter = fuelRemaining;

                var json = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"http://10.0.2.2:5183/Vehicles/PutVehicle/{vehicle.VehicleID}", content);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Vehicle fuel left updated successfully.", "OK");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to update vehicle fuel left. Server responded with: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }


        private async Task SetSourceToCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    SourceLocation = $"{location.Latitude}, {location.Longitude}";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        public async Task<List<TripDetail>> GetTripDetails()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/GetRecords");
                if (response.IsSuccessStatusCode)
                {
                    var records = await response.Content.ReadAsStringAsync();
                    var Triprecords = JsonConvert.DeserializeObject<List<TripDetail>>(records);
                    return Triprecords;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
