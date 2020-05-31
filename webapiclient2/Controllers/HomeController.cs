using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using webapiclient2.Factory;
using webapiclient2.Models;
using webapiclient2.Utility;

namespace webapiclient2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<MySettingsModel> appSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var data = await ApiClientFactory.Instance.GetFlights();
            List<Flights> remainingFlights = new List<Flights>();
            foreach(var cc in data)
            {
                if(cc.Date > DateTime.Now && cc.RemainingSeats != 0)
                {
                    remainingFlights.Add(cc);
                }
            }
            return View(remainingFlights);
        }

        public async Task<IActionResult> OneFlight(int id)
        {
            var data = await ApiClientFactory.Instance.GetFlight(id);
            return View(data);
        }

        public async Task<IActionResult> Bookings()
        {
            
            var data = await ApiClientFactory.Instance.GetBookings();
            
            var avions = await ApiClientFactory.Instance.GetFlights();
            
            List<Bookings> list = new List<Bookings>();
            foreach(var a in data)
            {
                if (a.PassengerId == 1)
                {
                    list.Add(a);
                }
               
            }

            ViewData["Flights"] = avions;
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
