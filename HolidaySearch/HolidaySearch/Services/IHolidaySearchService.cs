using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HolidaySearch.Models;



namespace HolidaySearch.Services
{
    public interface IHolidaySearchService
    {
        IEnumerable<HolidaySearch.Models.Holiday> Search(string departingFrom, string travelingTo, DateTime departureDate, int duration);
    }
}

