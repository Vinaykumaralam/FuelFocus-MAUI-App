using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Model
{
    public class VehicleReport
    {
        public int Hours { get; set; }
        public int Kilometer { get; set; }
    }
    public class CarouselContent
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public decimal Kilometer { get; set; }
        public decimal CHours { get; set; }
        public decimal Refilling { get; set; }
        public decimal JouneryTime { get; set; }
    }

}
