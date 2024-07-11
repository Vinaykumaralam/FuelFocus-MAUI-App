using FuelFocus.Model;
using FuelFocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FuelFocus.Services
{
    public class LoginService : ILoginService
    {
        public async Task<User> GetUser(long mobileNo)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new HttpClient();
                string url = "http://10.0.2.2:5183/api/Users/" + mobileNo;
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    User user = await response.Content.ReadFromJsonAsync<User>();
                    return await Task.FromResult(user);
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePassword(User user)
        {
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.PutAsJsonAsync("http://10.0.2.2:5183/api/Users", user );
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Country>> GetCountryCodeList()
        {
            List<Country> countryList = new List<Country>();
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("countrycodes.json");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                countryList = JsonSerializer.Deserialize<List<Country>>(contents);
                return countryList;
            }
            catch(Exception ex )
            {
                return countryList;
            }

        }
        public async Task<Vehicle> GetVehicle(long mobileNo)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new HttpClient();
                string url = "http://10.0.2.2:5183/Vehicles/GetVehicles";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    List<Vehicle> vehicles = await response.Content.ReadFromJsonAsync<List<Vehicle>>();
                    var vehicle = vehicles.Where(v => v.Mobile == mobileNo.ToString()).FirstOrDefault();
                    return await Task.FromResult(vehicle);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
