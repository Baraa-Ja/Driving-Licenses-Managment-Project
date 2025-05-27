using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_License_DataAccess_Layer.International_LicensesData
{
    public class clsinternationalLicenseData
    {
        public static bool GetInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static int AddNewApplication(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
        DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Update InternationalLicenses 
                             set IsActive=0
                             where DriverID=@DriverID;

                             insert into InternationalLicenses(ApplicationID,DriverID,IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                            values(@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    InternationalLicenseID = InsertedID;
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

            return InternationalLicenseID;
        }

        public static bool CheckLicenseAlreadyExists(int PersonID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from InternationalLicenses
                            join Applications on Applications.ApplicationID  = InternationalLicenses.ApplicationID
                            join People on People.PersonID = Applications.ApplicantPersonID
                            where People.PersonID = @PersonID;";
                            
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null)
                {
                    Isfound = true;

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

        public static DataTable GetInternationalLicenseTable(int PersonID)
        {
            DataTable InternationalLicenseTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select InternationalLicenseID as 'int.License ID', InternationalLicenses.ApplicationID as 'Applicaion ID', IssuedUsingLocalLicenseID as 'L.License ID',
                            IssueDate as 'Issue Date', ExpirationDate as 'Expiration Date', IsActive as 'Is Active' from InternationalLicenses
                            join Applications on Applications.ApplicationID = InternationalLicenses.ApplicationID
                            where Applications.ApplicantPersonID = @PersonID
                            ;";

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    InternationalLicenseTable.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseTable;
        }

        public static DataTable InterNationalLicenseList()
        {
            DataTable InternationalLicenseTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from InternationalLicenses;";

            SqlCommand command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    InternationalLicenseTable.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseTable;
        }

        public static bool UpdateLicense(int InternationalLicenseID, bool IsActive)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update InternationalLicenses
                            set IsActive = @IsActive
                            where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
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

            return RowsAffected > 0;
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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


            return InternationalLicenseID;
        }

        public static DataTable GetAllInternationalLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,DriverID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses 
                order by IsActive, ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
    }
}
