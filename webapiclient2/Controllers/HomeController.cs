using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using webapiclient2.Factory;
using webapiclient2.Models;
using webapiclient2.Utility;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

            var allFlights = from avion in data
                    where avion.Date > DateTime.Now && avion.RemainingSeats != 0
                    select avion;
            return View(allFlights);
        }

        [HttpGet]
        public ActionResult Login()
        {
            HttpContext.Session.SetInt32("ispassengerconnected", 0); //Nobody is connected

            return View();
        }

        public async Task<IActionResult> Login(Passengers p)
        {
            try
            {
                var data = await ApiClientFactory.Instance.GetPassengers();

                var GetID = from passenger in data
                            where passenger.Surname.Equals(p.Surname)
                            select passenger.PersonId;

                var PassengerID = GetID.ToArray();

                if (GetID != null)
                {
                    HttpContext.Session.SetInt32("idpassenger", PassengerID[0]);
                    HttpContext.Session.SetInt32("ispassengerconnected", 1); //Someone is connected

                    return RedirectToAction("Index", "Home", new { id = PassengerID[0] });
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> OneFlight(int id)
        {
            var data = await ApiClientFactory.Instance.GetFlight(id);
            ViewBag.flightID = id;
            string datetrunc = data.Date.ToString();
            ViewBag.DateTrunc = datetrunc.Substring(0,16);
            HttpContext.Session.SetInt32("FlightNumber", data.FlightNo);
            HttpContext.Session.SetInt32("FlightPrice", data.Price);
            return View(data);
        }

        public async Task<IActionResult> Bookings()
        {
            
            var data = await ApiClientFactory.Instance.GetBookings();
            
            var avions = await ApiClientFactory.Instance.GetFlights();
            
            List<Bookings> list = new List<Bookings>();

            int passengerID = HttpContext.Session.GetInt32("idpassenger").Value;
            var myBookings = from f in data
                       where f.PassengerId.Equals(passengerID)
                       select f;
                
            ViewData["Flights"] = avions;
            return View(myBookings);
        }

        public IActionResult Problems()
        {
            HttpContext.Session.SetInt32("ispassengerconnected", 0); //Nobody is connected 

            return View();
        }

        
        public async Task<IActionResult> BuyTickets()
        {
            //création dans la db d'un nouveau booking avec le prix  le prix la session pour l'Id du mec et l'id du vol
            Bookings Booked = new Bookings();
            Booked.FlightNo = (int)HttpContext.Session.GetInt32("FlightNumber");
            Booked.PassengerId = (int)HttpContext.Session.GetInt32("idpassenger");
            Booked.Price = (int)HttpContext.Session.GetInt32("FlightPrice");

            //Flights flights = new Flights();
            var flight = await ApiClientFactory.Instance.GetFlight(Booked.FlightNo);
            await ApiClientFactory.Instance.BuyOneTicket(Booked);
            
            try
            {
                await ApiClientFactory.Instance.PostAsyncFlight(flight);
            }
            catch (Exception)
            {

            }
            
            return RedirectToAction("Bookings", "Home");
        }

        public IActionResult Statistics()
        {
            return View();
        }

       
        public async Task<IActionResult> TotalPrice()
        {
            try
            {
                string Flight = Request.Form["TotalSale"];

                int FlightInt = Int32.Parse(Flight);
                int Total = await ApiClientFactory.Instance.GetTotalPriceForFlight(FlightInt);

                ViewBag.flight = FlightInt;
                ViewBag.total = Total;
            }catch(SystemException)
            {
                ViewBag.total = 0;
                ViewBag.flight = "not existant";
            }
            
            return View();
        }

        public async Task<IActionResult> AveragePrice()
        {
            try
            {
                string Destination = Request.Form["AverageSale"];

                float Total = await ApiClientFactory.Instance.GetAveragePriceForDestination(Destination.Substring(0, 3));

                ViewBag.destination = Destination.Substring(0, 3);
                ViewBag.total2 = Total;
            }
            catch (Exception)
            {
                ViewBag.total2 = 0;
                ViewBag.destination = "nowhere";
            }

            return View();
        }

        public async Task<IActionResult> ListTickets()
        {

            string Destination = Request.Form["ListTickets"];
            
            var list = await ApiClientFactory.Instance.GetStats(Destination.Substring(0,3));
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
