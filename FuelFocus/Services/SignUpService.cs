using FuelFocus.Model;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using FuelFocus.Models;

namespace FuelFocus.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly HttpClient _httpClient;
        public SignUpService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<bool> AddUser(User users)
        {
            try
            {
                Vehicle vehicle = new Vehicle
                {
                    UserName = users.Name,
                    Mobile = users.Mobile_No.ToString(),
                };         
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5183/api/Users", users);
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddVehicle(Vehicle vehicle)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://10.0.2.2:5183/Vehicles/PostVehicle", vehicle);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    // Log response for debugging
                    var errorResponse = await response.Content.ReadAsStringAsync();
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new HttpClient();
                string url = "http://10.0.2.2:5183/api/Users";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    List<User> users = await response.Content.ReadFromJsonAsync<List<User>>();
                    return await Task.FromResult(users);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> HashPassword(string password)
        {
            try
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] salt = new byte[16];
                    rng.GetBytes(salt);

                    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                    byte[] hash = pbkdf2.GetBytes(20);

                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"{ex}", "Ok");
                return password;
            }
        }

    }
}
