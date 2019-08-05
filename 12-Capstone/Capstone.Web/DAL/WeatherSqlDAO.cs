using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAO : IWeatherDAO
    {
        private readonly string connectionString;

        public WeatherSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Return the five day forecast for the selected park
        /// </summary>
        /// <param name="parkCode">The primary key of the selected park</param>
        /// <returns>List of five days worth of weather forecasts</returns>
        public List<WeatherViewModel> ParkWeather (string parkCode)
        {
            List<WeatherViewModel> Forecast = new List<WeatherViewModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM weather WHERE parkcode = @parkcode ORDER BY fivedayforecastvalue ASC;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkcode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        WeatherViewModel weather = new WeatherViewModel();

                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.LowTemp = Convert.ToInt32(reader["low"]);
                        weather.HighTemp = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        weather.FiveDayForecast = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.ImageSource = weather.Forecast + ".png";
                        
                        Forecast.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return Forecast;
        }
    }
}