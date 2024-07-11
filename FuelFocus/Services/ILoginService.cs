using FuelFocus.Model;
using FuelFocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Services
{
    public interface ILoginService
    {
        Task<User> GetUser(long mobileNo);
        Task<bool> UpdatePassword(User user);
        Task<List<Country>> GetCountryCodeList();
        Task<Vehicle> GetVehicle(long vehicleNo);
    }
}
