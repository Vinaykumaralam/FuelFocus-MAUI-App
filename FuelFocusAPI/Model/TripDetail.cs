using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelFocusAPI.Models
{
    public class TripDetail
    {
        [Key]
        public int TripId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public Decimal Distance { get; set; }
        public Decimal FuelConsumed { get; set; }
        public Decimal FuelRemaining { get; set; }
        public Decimal TotalFuel { get; set; }
        public DateTime StartDateTime { get; set; }
        public string JourneyTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int VehicleID { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }
}
