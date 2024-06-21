using System;
using System.IO;
using System.Linq;
using HolidaySearch.Models;
using HolidaySearch.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidaySearch.Tests
{
    [TestClass]
    public class HolidaySearchServiceTests
    {
        private HolidaySearchService _service;

        [TestInitialize]
        public void Setup()
        {
            // Adjust paths according to your project structure and ensure JSON files are present
            string projectPath = @"C:\Users\musta\source\repos\HolidaySearch\HolidaySearch\HolidaySearch\Tests";
            string flightDataPath = Path.Combine(projectPath, "Flights.json");
            string hotelDataPath = Path.Combine(projectPath, "Hotels.json");


            _service = new HolidaySearchService(flightDataPath, hotelDataPath);
        }

        [TestMethod]
        public void TestCustomer1()
        {
            string departingFrom = "MAN";
            string travelingTo = "AGP";
            DateTime departureDate = new DateTime(2023, 7, 01);
            int duration = 7;

            

            var results = _service.Search(departingFrom, travelingTo, departureDate, duration).ToList();

            //Assert.IsTrue(results.Any(), $"No holidays found for input: {departingFrom}, {travelingTo}, {departureDate}, {duration}");


            Assert.IsTrue(results.Any(), $"No holidays found for input: {departingFrom}, {travelingTo}, {departureDate}, {duration}");

            if (results.Any())
            {
                // Additional assertions to verify specific details of the first result
                int expectedFlightId = 2;
                int expectedHotelId = 9;
                int actualFlightId = results.First().Flight.Id;
                int actualHotelId = results.First().Hotel.Id;
                Assert.AreEqual(expectedFlightId, results.First().Flight.Id, $"Expected Flight ID: {expectedFlightId}");
                Assert.AreEqual(expectedHotelId, results.First().Hotel.Id, $"Expected Hotel ID: {expectedHotelId}");
            }

        }

        [TestMethod]
        public void TestCustomer2()
        {
            string departingFrom = "Any";
            string travelingTo = "PMI";
            DateTime departureDate = new DateTime(2023, 06, 15);
            int duration = 10;


            var results = _service.Search(departingFrom, travelingTo, departureDate, duration).ToList();

            Assert.IsTrue(results.Any(), "No holidays found"); // Ensure holidays are found
            if (results.Any())
            {
                // Additional assertions to verify specific details of the first result
                int expectedFlightId = 6;
                int expectedHotelId = 5;
                int actualFlightId = results.First().Flight.Id;
                int actualHotelId = results.First().Hotel.Id;
                Assert.AreEqual(expectedFlightId, results.First().Flight.Id, $"Expected Flight ID: {expectedFlightId}");
                Assert.AreEqual(expectedHotelId, results.First().Hotel.Id, $"Expected Hotel ID: {expectedHotelId}");
            }
           
        }

        [TestMethod]
        public void TestCustomer3()
        {
            string departingFrom = "Any";
            string travelingTo = "LPA";
            DateTime departureDate = new DateTime(2022, 11, 10);
            int duration = 14;

            

            var results = _service.Search(departingFrom, travelingTo, departureDate, duration).ToList();

            Assert.IsTrue(results.Any(), "No holidays found"); // Ensure holidays are found

           
            if (results.Any())
            {
                // Additional assertions to verify specific details of the first result
                int expectedFlightId = 7;
                int expectedHotelId = 6;
                int actualFlightId = results.First().Flight.Id;
                int actualHotelId = results.First().Hotel.Id;
                Assert.AreEqual(expectedFlightId, results.First().Flight.Id, $"Expected Flight ID: {expectedFlightId}");
                Assert.AreEqual(expectedHotelId, results.First().Hotel.Id, $"Expected Hotel ID: {expectedHotelId}");
            }
        }
    }
}
