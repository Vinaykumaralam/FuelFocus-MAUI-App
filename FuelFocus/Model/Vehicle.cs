using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Models
{
    public class Vehicle
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public int VehicleID { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string CompanyName { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public double FuelTankVolume { get; set; }
        public double VehicleMilage { get; set; }
        public double CurrentOdoMeter { get; set; }  //  attribute used as Current Fuel Left in whole project instead of odometer.

        public byte[] CarImage { get; set; }
        public byte[] UserProfileImage { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }

}
