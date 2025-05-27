using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace Driver_License_DataAccess_Layer
{
    public class clsPeopleData
    {

        public static bool GetPersonInfoByID(int PersonID, ref string PersonNationalNo, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Email, ref string Phone, ref int NationalityCountryID, ref string ImagePath)
        {   
            bool IsFound = false;
            
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString);

            string Query = "SELECT * FROM PEOPLE WHERE PERSONID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    PersonNationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    ThirdName = (string)Reader["ThirdName"];
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor = (Byte)Reader["Gendor"];
                    Address = (string)Reader["Address"];
                    Email = (string)Reader["Email"];
                    Phone = (string)Reader["Phone"];
                    NationalityCountryID = (int)Reader["NationalityCountryID"];

                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    IsFound = false;

                }

                Reader.Close();
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

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref Byte Gendor, ref string Address, ref string Email,
            ref string Phone, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from People Where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (Byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                else
                {
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
        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, short Gendor, string Email, string Phone,
            string Address,DateTime DateOfBirth, int NationalityCountryID, string ImagePath)
        {
            // IF Person Found Return PersondID And If Not Return -1
            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            string Query = @"INSERT INTO PEOPLE (NationalNo, FirstName, SecondName, ThirdName, LastName,
                            DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                            Values(@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName,
                            @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", LastName);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", Gendor);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                clsEventLogHandler.ExeptionsEventLog(ex.ToString(), EventLogEntryType.Information);

            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePersonInfo(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOFBirth , short Gendor, string Address,
            string Email, string Phone, int NationalityCountryID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"UPDATE PEOPLE
                            SET NationalNo = @NationalNo,
                                FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName,
                                DateOfBirth = @DateOfBirth,
                                Gendor = @Gendor,
                                Address = @Address,
                                Email = @Email,
                                Phone = @Phone,
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath = @ImagePath

                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOFBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                RowsAffected =  command.ExecuteNonQuery();
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

        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"DELETE FROM PEOPLE WHERE PERSONID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool CheckPersonExists(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT FOUND = 1 FROM PEOPLE  WHERE PERSONID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                isFound = Reader.HasRows;

                Reader.Close();
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

        public static DataTable GetAllPeople()
        {
            DataTable PeopleDataTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth , Gendor = 
                            CASE
                                WHEN Gendor=0 THEN 'Male'
                                WHEN Gendor=1 THEN 'Female'
                                ELSE 'Unknown'
                            END
                            ,Address, Phone, Email, Countries.CountryName As Nationality, ImagePath
                            from people
                            join Countries on People.NationalityCountryID = Countries.CountryID;";

            SqlCommand command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if(Reader.HasRows)
                {
                    PeopleDataTable.Load(Reader);
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

            return PeopleDataTable;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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


    }
}
