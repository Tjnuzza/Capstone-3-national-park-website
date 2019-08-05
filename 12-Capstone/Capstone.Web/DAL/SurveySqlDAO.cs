using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private readonly string connectionString;

        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Enters the user's survey into the database (which has already been verified in the controller)
        /// </summary>
        /// <param name="survey">The user's completed survey</param>
        public void SaveSurvey(SurveyViewModel survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Dictionary<string, int> GetSurveyResults()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT park.parkName, COUNT(*) AS park_count FROM survey_result " +
                    "JOIN park ON park.parkCode = survey_result.parkCode " +
                    "GROUP BY park.parkName " +
                    "ORDER BY COUNT(survey_result.parkCode) DESC;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        results.Add(Convert.ToString(reader["parkName"]), Convert.ToInt32(reader["park_count"]));
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }

            return results;
        }
    }
}