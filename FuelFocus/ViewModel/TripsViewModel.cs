using FuelFocus.Model;
using FuelFocus.View;
using Newtonsoft.Json;
using Syncfusion.Maui.Core.Carousel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FuelFocus.ViewModel
{
    public class TripsViewModel : BindableObject
    {
        //private string BaseUrl = "http://10.0.2.2:5183";
        private string BaseUrl = "http://10.0.2.2:5183";
        // private readonly API_service api;
        public ICommand NextCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand GetTripsCommand { get; set; }
        private ObservableCollection<TripDetail> trips;

        public ObservableCollection<TripDetail> Trips
        {
            get => trips;
            set
            {
                trips = value;
                OnPropertyChanged();
            }
        }
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged();
                }
            }
        }
        public TripsViewModel()
        {
            Trips = new ObservableCollection<TripDetail>();
            NextCommand = new Command<TripDetail>(OnNextTapped);
            GetTripsCommand = new Command(async() => await GetRecentTrips());
            GoBackCommand = new Command(async () => await GoBack());
            GetRecentTrips();
        }
        private async void OnNextTapped(TripDetail item)
        {
            try
            {

                await Application.Current.MainPage.Navigation.PushAsync(new TripDetailPage(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task GetRecentTrips()
        {
            IsLoading = true;
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
                    Trips = new ObservableCollection<TripDetail>(tripList);
                    if(Trips.Count == 0)
                    {
                        IsBusy = true;
                        IsLoading = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to get records:{ex.Message}", "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
