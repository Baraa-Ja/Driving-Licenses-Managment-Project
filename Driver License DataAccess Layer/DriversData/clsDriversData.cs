using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_License_DataAccess_Layer
{
    public class clsDriversData
    {
        public static bool GetDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from Drivers where DriverID  = @DriverID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    Isfound = false;
                }
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;

        }

        public static bool GetDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from Drivers where PersonID  = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    Isfound = false;
                }
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;

        }


        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into Drivers(PersonID, CreatedByUserID, CreatedDate)
                            values(@PersonID,@CreatedByUserID,@CreatedDate);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    DriverID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                connection.Close();
            }

            return DriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //we dont update the createddate for the driver.
            string query = @"Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsDriverExists(int PersonID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    PersonID = (int)reader["PersonID"];

                }
                else
                {
                    Isfound = false;
                }
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable Dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT dbo.Drivers.DriverID as 'DriverID', dbo.Drivers.PersonID as 'PersonID',
                           dbo.People.NationalNo as 'National No', dbo.People.FirstName + ' ' + dbo.People.SecondName +
                           ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName,
                           dbo.Drivers.CreatedDate as 'Date', (SELECT  COUNT(LicenseID) AS 'Active Licenses'
                           FROM dbo.Licenses WHERE (IsActive = 1) AND (DriverID = dbo.Drivers.DriverID)) AS 'ActiveLicenses'
                           FROM dbo.Drivers INNER JOIN dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID;";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    Dt.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                connection.Close();
            }

            return Dt;
        }

    }
}
