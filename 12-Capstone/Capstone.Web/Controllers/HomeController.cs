using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        public INatParkDAO parkDAO;
        public IWeatherDAO weatherDAO;

        // Remember that in order to do dependency injection, we need to add the DAOs in the constructor
        // Otherwise we need to pass in the strings manually
        public HomeController(INatParkDAO natParkDAO, IWeatherDAO weatherDAO)
        {
            this.parkDAO = natParkDAO;
            this.weatherDAO = weatherDAO;
        }

        /// <summary>
        /// The home page of our website, showing a brief overview of all parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<ParkViewModel> parks = parkDAO.GetAllParks();
            return View(parks);
        }

        /// <summary>
        /// Detail page for a single park, including weather report
        /// </summary>
        /// <param name="park">Park ID string for a single park</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Detail(string park)
        {
            ParkViewModel newPark = parkDAO.GetPark(park);
            newPark.Weather = weatherDAO.ParkWeather(park);
            newPark.ConversionChoice = GetPreferences();
            return View(newPark);
        }
        #region Preferences and session
        /// <summary>
        /// The page where the user decides if they want temperatures in celsius or fahrenheit (default to fahrenheit)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Preferences()
        {
            Preferences pref = new Preferences()
            {
                PreferredTemperature = GetPreferences()
            };
            return View(pref);
        }

        /// <summary>
        /// Temperature preference is set here
        /// </summary>
        /// <param name="pref">Object representing the user's temperature preference</param>
        /// <returns>Redirect to home page</returns>
        [HttpPost]
        public IActionResult Preferences(Preferences pref)
        {
            SetPreferences(pref.PreferredTemperature);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Get preferences cookie
        /// </summary>
        /// <returns></returns>
        private string GetPreferences()
        {
            return HttpContext.Session.GetString("Preferences") ?? "";
        }

        /// <summary>
        /// Set preferences cookie
        /// </summary>
        /// <param name="pref"></param>
        private void SetPreferences(string pref)
        {
            HttpContext.Session.SetString("Preferences", pref);
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}