namespace FuelFocus.ViewModel
{
    using FuelFocus.Model;
    using FuelFocus.View;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Microsoft.Maui.Controls;
    using Microsoft.Maui.Media;
    using FuelFocus.Models;
    using System.Net.Http.Json;
    using FuelFocus.Services;

    public class VehicleViewModel : INotifyPropertyChanged
    {
        private static VehicleViewModel _instance;
        private readonly ILoginService _loginService;
        public ICommand GoBackCommand { get; set; }
        public static VehicleViewModel Instance => _instance ??= new VehicleViewModel();

        private readonly HttpClient _httpClient;
        private readonly Uri BaseUrl = new Uri("http://10.0.2.2:5183/Vehicles");

        public VehicleViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = BaseUrl };
            _loginService= new LoginService();
            Vehicles = new ObservableCollection<Vehicle>();
            SaveCommand = new Command(async () => await SaveVehicle());
            GoBackCommand = new Command(async () => await GoBack());
            NavigateToRoutePageCommand = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new RoutePage()));
            ChangeCarImageCommand = new Command(async () => await ChangeCarImage());
            ChangeProfileImageCommand = new Command(async () => await ChangeProfileImage());
            //Task.Run(async () => await FetchVehiclesFromApi());
        }

        public ObservableCollection<Vehicle> Vehicles { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand NavigateToRoutePageCommand { get; }
        public ICommand ChangeCarImageCommand { get; }
        public ICommand ChangeProfileImageCommand { get; }

        private byte[] _carImage;
        public byte[] CarImage
        {
            get => _carImage;
            set
            {
                _carImage = value;
                OnPropertyChanged(nameof(CarImage));
            }
        }

        private byte[] _userProfileImage;
        public byte[] UserProfileImage
        {
            get => _userProfileImage;
            set
            {
                _userProfileImage = value;
                OnPropertyChanged(nameof(UserProfileImage));
            }
        }
        //string mobileNo = Preferences.Get("UserMobileNo", "9392658469");
        public async Task FetchVehiclesFromApi()
        {
            try
            {
                string mobileNo = Preferences.Get("UserMobileNo", "9392658469");
                HttpResponseMessage response = await _httpClient.GetAsync($"{BaseUrl}/GetVehicles");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(data);
                    var filterVehicle = vehicles.Where(x => x.Mobile == mobileNo).ToList();
                    Vehicles = new ObservableCollection<Vehicle>(filterVehicle);
                    OnPropertyChanged(nameof(Vehicles));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error fetching vehicles", ex.Message, "Ok");
            }
        }

        private async Task SaveVehicle()
        {
            try
            {
                var vehicle = Vehicles.FirstOrDefault();
                if (vehicle != null)
                {
                    HttpResponseMessage getResponse = await _httpClient.GetAsync($"{BaseUrl}/GetVehicle/{vehicle.VehicleID}");
                    if (getResponse.IsSuccessStatusCode)
                    {
                        var existingVehicleData = await getResponse.Content.ReadAsStringAsync();
                        var existingVehicle = JsonConvert.DeserializeObject<Vehicle>(existingVehicleData);

                        if (vehicle.CarImage == null && existingVehicle != null)
                        {
                            vehicle.CarImage = existingVehicle.CarImage;
                        }

                        if (vehicle.UserProfileImage == null && existingVehicle != null)
                        {
                            vehicle.UserProfileImage = existingVehicle.UserProfileImage;
                        }
                    }

                    var json = JsonConvert.SerializeObject(vehicle);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response;
                    HttpResponseMessage httpResponse;

                    if (vehicle.VehicleID == 0)
                    {
                        response = await _httpClient.PostAsync($"{BaseUrl}/PostVehicle", content);
                    }
                    else
                    {
                        vehicle.Updated_On = DateTime.Now;
                        response = await _httpClient.PutAsync($"{BaseUrl}/PutVehicle/{vehicle.VehicleID}", content);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        if (vehicle.VehicleID != 0)
                        {
                            var user = await _loginService.GetUser(long.Parse(vehicle.Mobile));
                            user.Name = vehicle.UserName;
                            httpResponse = await _httpClient.PutAsJsonAsync("http://10.0.2.2:5183/api/Users", user);
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                Preferences.Set("UserName", vehicle.UserName);
                            }
                        }
                        await Application.Current.MainPage.DisplayAlert("Save", "Vehicle information saved successfully!", "OK");
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Failed to save vehicle information.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private async Task ChangeCarImage()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    CarImage = memoryStream.ToArray();

                    var vehicle = Vehicles.FirstOrDefault();
                    if (vehicle != null)
                    {
                        var updatedVehicle = await SaveVehicleImage(vehicle.VehicleID, CarImage, null);
                        if (updatedVehicle != null)
                        {
                            var index = Vehicles.IndexOf(vehicle);
                            Vehicles[index] = updatedVehicle;
                            OnPropertyChanged(nameof(Vehicles)); 
                        }
                    }
                }
            }
        }

        private async Task ChangeProfileImage()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    UserProfileImage = memoryStream.ToArray();

                    var vehicle = Vehicles.FirstOrDefault();
                    if (vehicle != null)
                    {
                        var updatedVehicle = await SaveVehicleImage(vehicle.VehicleID, null, UserProfileImage);
                        if (updatedVehicle != null)
                        {
                            var index = Vehicles.IndexOf(vehicle);
                            Vehicles[index] = updatedVehicle;
                            OnPropertyChanged(nameof(Vehicles)); 
                        }
                    }
                }
            }
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task<Vehicle> SaveVehicleImage(int vehicleId, byte[] carImage = null, byte[] userProfileImage = null)
        {
            try
            {
                var content = new MultipartFormDataContent();

                if (carImage != null)
                {
                    var carImageContent = new ByteArrayContent(carImage);
                    carImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    content.Add(carImageContent, "carImage", "carImage.jpg");
                }

                if (userProfileImage != null)
                {
                    var profileImageContent = new ByteArrayContent(userProfileImage);
                    profileImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    content.Add(profileImageContent, "userProfileImage", "userProfileImage.jpg");
                }

                var response = await _httpClient.PutAsync($"{BaseUrl}/UploadImage/UploadImage/{vehicleId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var updatedVehicleJson = await response.Content.ReadAsStringAsync();
                    var updatedVehicle = JsonConvert.DeserializeObject<Vehicle>(updatedVehicleJson);
                    return updatedVehicle;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save image. Server response: {errorMessage}", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred while saving image: {ex.Message}", "OK");
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
