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
    public class clsLDLApplicationData
    {
        public static bool GetApplicationByApplicationID(int LDLApplicationID, ref int LicenseClassID, ref int ApplicationID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    Isfound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    ApplicationID = (int)reader["ApplicationID"];

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
                connection.Close() ;
            }

            return Isfound;
        }

        public static bool FindLDLApplicationByApplicationID(int ApplicationID, ref int LicenseClassID, ref int LDLApplicationID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from LocalDrivingLicenseApplications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    LDLApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];

                }
                else
                {
                    Isfound = false;
                }
            }
            catch
            {
                ;
            }
            finally
            {
                connection.Close();
            }

            return Isfound;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus,
            DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into Applications(ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                            values(@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ApplicationID = InsertedID;
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }


        public static int AddNewLDLApplication(int LicenseClassID, int ApplicationID)
        {
            int lDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into LocalDrivingLicenseApplications(LicenseClassID, ApplicationID)
                            values(@LicenseClassID, @ApplicationID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    lDLApplicationID = InsertedID;
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

            return lDLApplicationID;
        }

        public static bool UpdateApplication(int applicationID, int applicantPersonID, DateTime ApplicationDate, int applicationTypeID, Byte ApplicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update Applications
                            set applicantPersonID = @applicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                applicationTypeID = @applicationTypeID,
                                ApplicationStatus = @ApplicationStatus,
                                lastStatusDate = @lastStatusDate,
                                paidFees = @paidFees,
                                createdByUserID = @createdByUserID
                            where applicationID = @applicationID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@applicantPersonID", applicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@applicationTypeID", applicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate", lastStatusDate);
            command.Parameters.AddWithValue("@paidFees", paidFees);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);
            command.Parameters.AddWithValue("@applicationID", applicationID);


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

        public static bool UpdateLDLApplication(int LDLApplicationID, int LicenseClassID, int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update LocalDrivingLicenseApplications
                            set LicenseClassID = @LicenseClassID,
                                ApplicationID = @ApplicationID
                            where LocalDrivingLicenseApplicationID = @LDLApplicationID;";
            
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

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

        public static bool DeleteLDLApplication(int LDLApplication)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Delete from LocalDrivingLicenseApplications Where LocalDrivingLicenseApplicationID = @LDLApplication";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LDLApplication", LDLApplication);

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

        public static int GetPassedTestcount(int LDLApplicationID)
        {
            int Count = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select 
                            (select COUNT(TestAppointments.TestTypeID) 
                            from	 TestAppointments  
                            join Tests on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            where (TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) and (Tests.TestResult = 1)) AS PassedTestCount
                            from LocalDrivingLicenseApplications
                            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    //LDLApplicationID = (int)reader["LDLApplicationID"];
                    Count = (int)reader["PassedTestCount"];
                }
                else
                {
                    Count = -1;
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

            return Count;
        }

        public static DataTable GetLDLApplications()
        {
            DataTable LDLApplicationTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from LocalDrivingLicenseApplications_View
                            order by ApplicationDate Desc;";
                                                
            SqlCommand command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    LDLApplicationTable.Load(Reader);
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

            return LDLApplicationTable;
        }

        public static bool CheckLicenseAlreadyExists(int PersonID, int LicenseClassID)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select 1 from LocalDrivingLicenseApplications
                            join Applications on Applications.ApplicationID  = LocalDrivingLicenseApplications.ApplicationID
                            join People on People.PersonID = Applications.ApplicantPersonID
                            where LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID and People.PersonID = @PersonID
                            and(Applications.ApplicationStatus = 1 or Applications.ApplicationStatus = 3);";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            AND(Tests.TestResult = 1)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;

        }


        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

            return IsFound;

        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
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

            return TotalTrialsPerTest;

        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
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

            return Result;

        }



    }
}
