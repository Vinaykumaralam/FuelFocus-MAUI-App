using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Model
{
   public class Country
   {
        public string CountryName { get; set; }
        public int CountryCode {  get; set; }
        public string DisplayName => $"+{CountryCode}";
    }
}
