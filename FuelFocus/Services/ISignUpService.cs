using FuelFocus.Model;
using FuelFocus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Services
{
    public interface ISignUpService
    {
        Task<bool> AddUser(User user);
        Task<bool> AddVehicle(Vehicle vehicle);
        Task<List<User>>  GetUsers();
        Task<string> HashPassword(string password);
    }
}
