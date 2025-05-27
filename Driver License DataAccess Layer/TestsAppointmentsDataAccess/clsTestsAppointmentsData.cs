using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_License_DataAccess_Layer.VisionTestAppointmentsDataAccess
{
    public class clsTestsAppointmentsData
    {
        public static bool GetTestAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int localDrivingLicenseApplicationID,
                ref DateTime appointmentDate, ref decimal paidFees, ref int createdByUserID, ref Boolean IsLocked)

        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from TestAppointments where TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Isfound = true;

                    TestTypeID = (int)reader["TestTypeID"];
                    localDrivingLicenseApplicationID = (int)reader["localDrivingLicenseApplicationID"];
                    appointmentDate = (DateTime)reader["appointmentDate"];
                    paidFees = (decimal)reader["paidFees"];
                    createdByUserID = (int)reader["createdByUserID"];
                    IsLocked = (Boolean)reader["IsLocked"];
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

        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID,ref int TestAppointmentID
            , ref DateTime AppointmentDate,ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    //if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    //    RetakeTestApplicationID = -1;
                    //else
                    //    RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


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

            return isFound;
        }

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            decimal PaidFees, int CreatedByUserID, Boolean IsLocked)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into TestAppointments(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked)
                            values(@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    TestAppointmentID = InsertedID;
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, DateTime AppointmentDate, Boolean IsLocked)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update TestAppointments
                            set AppointmentDate = @AppointmentDate,
                                IsLocked = @IsLocked
                            where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }

        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


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

        public static DataTable TestAppointmentsList(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            DataTable TestAppointmentsDataTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select TestAppointments.TestAppointmentID as 'Appointment ID', TestAppointments.AppointmentDate as 'Appointment Date',
                            TestAppointments.PaidFees as 'Paid Fees', TestAppointments.IsLocked as 'Is Locked' 
                            from TestAppointments
                            join TestTypes on TestTypes.TestTypeID = TestAppointments.TestTypeID
                            where TestTypes.TestTypeID = @TestTypeID and TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            //    string Query = @"select TestAppointments.TestAppointmentID as 'Appointment ID', TestAppointments.AppointmentDate as 'Appointment Date',
            //                    TestAppointments.PaidFees as 'Paid Fees', IsLocked  =
            //CASE
            //                        WHEN IsLocked=0 THEN 'true'
            //                        WHEN IsLocked=1 THEN 'false'
            //                        ELSE 'Unknown'
            //                    END 
            //                    from TestAppointments
            //                    join TestTypes on TestTypes.TestTypeID = TestAppointments.TestTypeID
            //                    where TestTypes.TestTypeID = 1;";

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    TestAppointmentsDataTable.Load(Reader);
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

            return TestAppointmentsDataTable;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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


            return TestID;

        }

        public static bool CheckAppointmentAlreadyExists(int TestTypeID, int LocalDrivingLicenseApplicationID, bool IsLocked)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select TestAppointments.TestAppointmentID as 'Appointment ID', TestAppointments.AppointmentDate as 'Appointment Date',
                            TestAppointments.PaidFees as 'Paid Fees', TestAppointments.IsLocked as 'Is Locked' 
                            from TestAppointments
                            join TestTypes on TestTypes.TestTypeID = TestAppointments.TestTypeID
                            where TestTypes.TestTypeID = @TestTypeID and TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
							and TestAppointments.IsLocked = @IsLocked;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);


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

        public static bool CheckPersonAlreadyPassedTest(int TestTypeID, int LocalDrivingLicenseApplicationID, bool TestResult)
        {
            bool Isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Tests.TestID, Tests.TestAppointmentID, Tests.TestResult from Tests 
                            join TestAppointments on TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            join TestTypes on TestTypes.TestTypeID = TestAppointments.TestTypeID
                            where Tests.TestResult = @TestResult and TestTypes.TestTypeID = @TestTypeID
                            and TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestResult", TestResult);


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

    }
}
