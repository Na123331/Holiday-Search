using System;
using System.Collections.Generic;
using System.Linq;
using HolidaySearch.Models;
using Newtonsoft.Json;

namespace HolidaySearch.Services
{
    public class HolidaySearchService : IHolidaySearchService
    {
        private readonly List<Flight> _flights;
        private readonly List<Hotel> _hotels;

        public HolidaySearchService(string flightDataPath, string hotelDataPath)
        {
            try
            {
                string flightJson = System.IO.File.ReadAllText(flightDataPath);
                string hotelJson = System.IO.File.ReadAllText(hotelDataPath);

                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc // Optionally set timezone handling
                };

                _flights = JsonConvert.DeserializeObject<List<Flight>>(flightJson, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd"  // Adjust format to match your JSON date format
                });
                _hotels = JsonConvert.DeserializeObject<List<Hotel>>(hotelJson, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd"  // Adjust format to match your JSON date format
                });

                Console.WriteLine($"Flights loaded: {_flights.Count}");
                Console.WriteLine($"Hotels loaded: {_hotels.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                throw; // Propagate the exception to indicate data loading failure
            }
        }

        public IEnumerable<Holiday> Search(string departingFrom, string travelingTo, DateTime departureDate, int duration)
        {
            Console.WriteLine($"Searching holidays: From={departingFrom}, To={travelingTo}, Date={departureDate.ToShortDateString()}, Duration={duration}");

            var matchingFlights = _flights.Where(f => (departingFrom == "Any" || f.From == departingFrom) &&
                                                      f.To == travelingTo &&
                                                      f.DepartureDate.Date == departureDate.Date).ToList();

            Console.WriteLine($"Matching flights count: {matchingFlights.Count}");
            foreach (var flight in matchingFlights)
            {
                Console.WriteLine($"Matched Flight: {flight.Id}, From: {flight.From}, To: {flight.To}, Date: {flight.DepartureDate.ToShortDateString()}");
            }

            var matchingHotels = _hotels.Where(h => h.LocalAirports != null &&
                                                    h.LocalAirports.Contains(travelingTo) &&
                                                    h.ArrivalDate.Date == departureDate.Date &&
                                                    h.Nights == duration).ToList();

            Console.WriteLine($"Matching hotels count: {matchingHotels.Count}");
            foreach (var hotel in matchingHotels)
            {
                Console.WriteLine($"Matched Hotel: {hotel.Id}, Name: {hotel.Name}, Arrival: {hotel.ArrivalDate.ToShortDateString()}, Nights: {hotel.Nights}");
            }

            List<Holiday> holidays = new List<Holiday>();

            foreach (var flight in matchingFlights)
            {
                // This line might be redundant since we've already filtered the hotels
                var validHotels = matchingHotels.Where(h => h.LocalAirports.Contains(flight.To)).ToList();
                Console.WriteLine($"Flight ID {flight.Id} valid hotels count: {validHotels.Count}");

                foreach (var hotel in validHotels)
                {
                    holidays.Add(new Holiday { Flight = flight, Hotel = hotel });
                    Console.WriteLine($"Holiday Added: Flight ID={flight.Id}, Hotel ID={hotel.Id}");
                }
            }

            var sortedHolidays = holidays.OrderBy(h => h.TotalPrice).ToList();
            Console.WriteLine($"Number of holidays found: {sortedHolidays.Count}");

            return sortedHolidays;
        }
    }
}
