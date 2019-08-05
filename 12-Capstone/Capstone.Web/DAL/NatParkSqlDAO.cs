using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class NatParkSqlDAO : INatParkDAO
    {
        private readonly string connectionString;
        
        public NatParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Returns a list of all parks in the database
        /// </summary>
        /// <returns></returns>
        public List<ParkViewModel> GetAllParks()
        {
            List<ParkViewModel> parks = new List<ParkViewModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM park;";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ParkViewModel park = new ParkViewModel
                        {
                            ParkName = Convert.ToString(reader["parkName"]),
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVisitors = Convert.ToInt32(reader["annualVisitorCount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            EntryFee = Convert.ToInt32(reader["entryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
                        };
                        park.ImageSource = park.ParkCode + ".jpg";

                        parks.Add(park);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return parks;
        }
        
        /// <summary>
        /// Get information for a single park
        /// </summary>
        /// <param name="parkCode">Primary key of the park in our database</param>
        /// <returns>One completed park model</returns>
        public ParkViewModel GetPark(string parkCode)
        {
            ParkViewModel park = new ParkViewModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM park WHERE parkcode = @parkCode;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitors = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                        park.ImageSource = park.ParkCode + ".jpg";
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return park;
        }
    }
}