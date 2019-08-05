using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class WeatherViewModel
    {
        public int HighTemp { get; set; }
        public int LowTemp { get; set; }

        /// <summary>
        /// What the weather is expected to be that day
        /// </summary>
        public string Forecast { get; set; }

        /// <summary>
        /// How many days out this forecast is for
        /// </summary>
        public int FiveDayForecast { get; set; }
        public string ParkCode { get; set; }
        public string ImageSource { get; set; }

        /// <summary>
        /// The advisory message for the weather
        /// </summary>
        /// <returns></returns>
        public string Advisory()
        {
            string message = "";
            switch(Forecast)
            {
                case "rain":
                    message += "Remember to pack rain gear and waterproof shoes.";
                    break;
                case "snow":
                    message += "Remember to pack snowshoes.";
                    break;
                case "thunderstorms":
                    message += "Seek shelter and avoid hiking on exposed ridges!";
                    break;
                case "sun":
                    message += "Remember to pack sunblock.";
                    break;
            }
            return message;
        }

        /// <summary>
        /// Advisory message for high and low temperatures, and large temperature differences
        /// </summary>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        public string TempAdvisory()
        {
            string message = "";
            if (HighTemp > 75)
            {
                message += "Bring an extra gallon of water! ";
            }
            if (LowTemp < 20)
            {
                message += "Be careful of exposure to frigid temperatures! ";
            }
            if (HighTemp - LowTemp > 20)
            {
                message += "Remember to wear breathable layers. ";
            }
            return message;
        }

        public int TemperatureConversion(int fahTemp)
        {
            return (fahTemp - 32) * 5 / 9;
        }
    }
}