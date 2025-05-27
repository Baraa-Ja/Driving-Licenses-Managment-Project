using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_DataAccess_Layer.LicenseClasses
{
    public class clsLicenseClassesDataAccess
    {
        public static bool FindLicenseClassByID(int LicenseClassID, ref string className, ref string classDescription, ref byte minimumAllowedAge,
            ref byte defaultValidityLength, ref decimal ClassFees)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LicenseClasses where LicenseClassID  = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    isfound = true;

                    className = (string)Reader["className"];
                    classDescription = (string)Reader["classDescription"];
                    minimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    defaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = (decimal)Reader["ClassFees"];

                }
                else
                {
                    isfound = false;
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

            return isfound;
        }

        public static bool FindLicenseClassByName(string className, ref int LicenseClassID, ref string classDescription, ref byte minimumAllowedAge,
            ref byte defaultValidityLength, ref decimal ClassFees)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LicenseClasses where ClassName  = @className";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@className", className);

            try
            {
                connection.Open();

                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    isfound = true;

                    LicenseClassID = (int)Reader["LicenseClassID"];
                    classDescription = (string)Reader["ClassDescription"];
                    minimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    defaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = (decimal)Reader["ClassFees"];

                }
                else
                {
                    isfound = false;
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

            return isfound;
        }

        public static DataTable GetAllClasses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses order by ClassName";

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
