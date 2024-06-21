using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Models
{
    public class Holiday
    {
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
        public decimal TotalPrice => Flight.Price + (Hotel.PricePerNight * Hotel.Nights);
    }
}

