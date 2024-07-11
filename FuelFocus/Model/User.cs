using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelFocus.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Mobile_No { get; set; }
        public string Password { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get;set; }
    }
}
