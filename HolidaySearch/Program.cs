using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace HolidaySearch.Models
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string jsonFilePath = @"C:\Users\musta\source\repos\HolidaySearch\HolidaySearch\Tests\Flights.json";


            try
            {
                // Read JSON file contents
                string jsonText = File.ReadAllText(jsonFilePath);

                // Deserialize JSON to list of Flight objects
                List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(jsonText, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd"  // Adjust format to match your JSON date format
                });

                // Print flights to verify
                foreach (var flight in flights)
                {
                    Console.WriteLine($"{flight.Airline} - {flight.From} to {flight.To} on {flight.DepartureDate}");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: File not found. {ex.Message}");
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
