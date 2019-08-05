using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkViewModel
    {
        /// <summary>
        /// Three or more letter ID code for the park, the primary key in the database
        /// </summary>
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int ElevationInFeet { get; set; }
        public int MilesOfTrail { get; set; }
        public int NumberOfCampsites { get; set; }
        public string InspirationalQuote { get; set; }
        public string InspirationalQuoteSource { get; set; }
        public string ParkDescription { get; set; }

        /// <summary>
        /// Entry fee, in dollars
        /// </summary>
        public int EntryFee { get; set; }

        /// <summary>
        /// Lots of interesting furry animals
        /// </summary>
        public int NumberOfAnimalSpecies { get; set; }
        public int YearFounded { get; set; }
        public string Climate { get; set; }
        public int AnnualVisitors { get; set; }

        /// <summary>
        /// The name of the image in the images folder (with a standard path)
        /// </summary>
        public string ImageSource { get; set; }

        /// <summary>
        /// The five day forecast, taken from database using our WeatherSqlDAO
        /// </summary>
        public IList<WeatherViewModel> Weather { get; set; }

        /// <summary>
        /// The user's choice of temperature scale, input via cookie and included in the viewmodel because of model binding rules
        /// </summary>
        public string ConversionChoice { get; set; }
    }
}