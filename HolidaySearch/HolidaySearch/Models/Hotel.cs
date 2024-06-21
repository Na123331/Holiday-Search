using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HolidaySearch.Models
{
    using Newtonsoft.Json;
    using System;
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }
        public decimal PricePerNight { get; set; }

        [JsonProperty("local_airports")]
        public List<string> LocalAirports { get; set; }
        public int Nights { get; set; }
    }
}
